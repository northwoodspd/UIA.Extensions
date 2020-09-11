#tool nuget:?package=JetBrains.dotCover.CommandLineTools&version=2020.1.4
#tool nuget:?package=NUnit.ConsoleRunner&version=3.11.1
#tool nuget:?package=NUnit.Extension.TeamCityEventListener&version=1.0.7
#addin nuget:?package=Cake.VersionReader&version=5.1.0
#addin nuget:?package=Cake.Incubator&version=5.1.0
#addin nuget:?package=Cake.Git&version=0.22.0
#addin nuget:?package=Cake.FileHelpers&version=3.3.0

///
/// Conventions
///
/// You have a single .sln at the root of the repository
/// The git remote is located at http://github.com/northwoodspd/{solutionName()}
/// Test projects have project names that end with {testAssemblyPattern}
/// Integration Test projects have project names that end with {integrationTestAssemblyPattern}
///

var testAssemblyPattern = "*.Units";
var integrationTestAssemblyPattern = "*.Tests.Integration";

var msBuildToolVersion = MSBuildToolVersion.VS2019;

var target         = Argument("target", "Default");
var configuration  = Argument("configuration", "Release");
var buildVerbosity = Argument("buildVerbosity", "quiet");
var buildNumber    = Argument("buildNumber", "1");

Verbosity msBuildVerbosity;
NuGetVerbosity nugetRestoreVerbosity;

switch (buildVerbosity)
{
    case "quiet":
        msBuildVerbosity = Verbosity.Quiet;
        nugetRestoreVerbosity = NuGetVerbosity.Quiet;
        break;
    case "normal":
        msBuildVerbosity = Verbosity.Normal;
        nugetRestoreVerbosity = NuGetVerbosity.Normal;
        break;
    default:
        throw new ArgumentException($"Invalid value '{buildVerbosity}' provided for buildVerbosity. Valid values are 'quiet' or 'normal'.");
}


///
/// //////////////////////////////////////////////////
///
///       Component Actions used in Cake Tasks
///
/// //////////////////////////////////////////////////
///

///
/// The path to the first solution found in the root directory of this repository.
///
Func<string> solutionPath = () => {
    var candidates = GetFiles("*.sln");

    if (candidates.Count != 1) {
        Error("Could not find a single candidate .sln file in the root directory.");
    }

    return candidates.ToList().First().GetFilename().ToString();
};


///
/// The solution name
///
Func<string> solutionName = () => {
    var path = solutionPath();

    return path.Replace(".sln", "");
};

///
/// <summary>
/// Clean All Directories in the Solution with Name
/// </summary>
///
Action<string> cleanAllDirectories = directoryName => {
    var solution = ParseSolution(solutionPath());

        var actions = new List<Action>();

        foreach(SolutionProject project in solution.Projects) {
            var projectPath = new FilePath(project.Path.ToString());

            if (projectPath.HasExtension)
            {
                DirectoryPath directory = projectPath.GetDirectory();

                actions.Add(() => {
                    deleteDirectory(directory, directoryName);
                });
            }
        }

        var options = new ParallelOptions {
            MaxDegreeOfParallelism = -1
        };

        Parallel.Invoke(options, actions.ToArray());
};

///
/// <summary>
/// Restore Nugets from the repository.
/// </summary>
///
Action nugetRestore = () =>
{
    NuGetRestore(solutionPath(), new NuGetRestoreSettings()
    {
        Verbosity = nugetRestoreVerbosity
    });
};

///
/// <summary>
/// Puts the build number into the assembly version.
/// </summary>
///
Action setAssemblyVersion = () =>
{
    var v = assemblyVersion();
    var a = new Version(v);

    var version = new Version(a.Major, a.Minor, Int32.Parse(buildNumber), 0).ToString();

    Information($"Using AssemblyVersion({version})");

    ReplaceTextInFiles("SolutionAssemblyInfo.cs", v, version);
};

///
/// <summary>
/// Safely Delete a Directory
/// </summary>
///
Action<DirectoryPath, string> deleteDirectory = (directory, segment) => {
    var subdirectory = new DirectoryPath($"{directory.FullPath}\\{segment}");
    if (DirectoryExists(subdirectory))
    {
        Information(logAction=>logAction("Deleting {0}", subdirectory));
        DeleteDirectory(subdirectory, new DeleteDirectorySettings {
                                   Recursive = true,
                                   Force = true
                               });
    }
    else
    {
        Information(logAction=>logAction("Skipping deletion of {0}", subdirectory));
    }
};

///
/// <summary>
/// Builds the Solution
/// </summary>
///
Action buildSolution = () => {
    MSBuild(solutionPath(), settings => {
        settings.SetConfiguration(configuration)
                .SetVerbosity(msBuildVerbosity)
                .UseToolVersion(msBuildToolVersion)
                .SetNodeReuse(false)
                .SetMaxCpuCount(0);
    });
};

///
/// <summary>
/// A strategy for discovering test projects
/// <summary>
///
Func<string, IEnumerable<string>> discoverTestAssemblies = (assemblyGlob) =>
{
    var testLibraries = GetDirectories(assemblyGlob).Select(dir => {
        var name = dir.Segments.ToList().Last();
        var dll = $"{name}/bin/{configuration}/{name}.dll";
        Information($"Including {dll}");

        return dll;
    });

    return testLibraries;
};

///
/// <summary>
/// Execute NUnit3 Tests
/// 1. glob to find test assemblies
/// 2. hard timeout for each test method
/// 3. timeout for the entire run
/// </summary>
///
Action<string, int, TimeSpan> executeNUnitTests = (dirGlob, testTimeout, toolTimeout) =>
{
    var settings = new NUnit3Settings {
        Framework = "net-4.0",
        Timeout = testTimeout,
        ToolTimeout = toolTimeout,
        X86 = true,
        TraceLevel = NUnitInternalTraceLevel.Verbose
    };
    var assemblies = discoverTestAssemblies(dirGlob);

    if (assemblies.ToList().Count > 0)
    {
        NUnit3(assemblies, settings);
    }
    else
    {
        Information("No integration test assemblies found, skipping.");
    }
};

///
/// <summary>
/// Execute NUnit3 Tests with DotCover given:
/// 1. glob to find test assemblies
/// 2. name for these tests which separates them from other coverage groups
/// 3. way to modify dotcover settings
/// 4. hard timeout for each test method
/// 5. timeout for the entire run
/// </summary>
///
Action<string, string, Action<DotCoverCoverSettings>, int, TimeSpan> executeTests = (dirGlob, suiteIdentifier, dotCoverSettingsModifier, testTimeout, toolTimeout) =>
{
    var coverageOutputFile = File($"./tools/output/{suiteIdentifier}-test-coverage.dcvr");

    try
    {
        var settings = new NUnit3Settings {
            Framework = "net-4.0",
            Timeout = testTimeout,
            ToolTimeout = toolTimeout,
            X86 = true,
            TraceLevel = NUnitInternalTraceLevel.Verbose
        };

        var dotCoverSettings = new DotCoverCoverSettings{
            ToolTimeout = TimeSpan.FromMinutes(15)
        };

        dotCoverSettings.WithFilter($"+:{solutionName()}*")
            .WithFilter("-:*.Tests")
            .WithFilter("-:type=*Resources*");

        dotCoverSettingsModifier(dotCoverSettings);

        var libs = discoverTestAssemblies(dirGlob);
        DotCoverCover(tool => tool.NUnit3(libs, settings), coverageOutputFile, dotCoverSettings);
    }
    finally
    {
        if (BuildSystem.IsRunningOnTeamCity)
        {
            TeamCity.ImportDotCoverCoverage(MakeAbsolute(coverageOutputFile), MakeAbsolute(Context.Tools.Resolve("dotcover.exe").GetDirectory()));
        }
    }
};

///
/// Version from AssemblyInfo
///
Func<string> assemblyVersion = () => {
    return ParseAssemblyInfo(new FilePath("SolutionAssemblyInfo.cs")).AssemblyVersion;
};

///
/// Get the current Git branch name
///
Func<string> gitBranch = () => {
    return GitBranchCurrent(Directory(".././")).FriendlyName;
};

///
/// Nuget Artifact Versions, with support for beta (non-master) releases.
///
Func<string> nugetVersion = () => {
    var version = assemblyVersion();

    return "master".Equals(gitBranch()) ? version : $"{version}-beta";
};

///
/// Pack the Nugets
///
Action nugetPack = () =>
{
    var nugetDir = "./nuget";

    var sln = ParseSolution(solutionPath());
    var slnProjects = sln.Projects.Where(pro => !pro.Name.Contains(".Units") && !pro.Name.Contains("TestApplication") && pro.Path.FullPath.EndsWith("proj"));

    if (DirectoryExists(nugetDir))
    {
        DeleteDirectory(nugetDir, new DeleteDirectorySettings { Recursive = true, Force = true });
    }

    CreateDirectory(nugetDir);

    foreach (var slnProject in slnProjects) {
        Information($"Packing {slnProject.Name}");

        var project = ParseProject(slnProject.Path);
        var dill = slnProject.Path.GetDirectory().CombineWithFilePath(File($"./bin/{configuration}/{project.AssemblyName}.dll"));
        var target = "lib/" + project.TargetFrameworkVersion.Replace(".", "").Replace("v", "net");

        var nuGetPackSettings   = new NuGetPackSettings {
            Id                       = project.AssemblyName,
            Version                  = nugetVersion(),
            Title                    = project.AssemblyName,
            Authors                  = new[] {"Northwoods Consulting Partners"},
            Owners                   = new[] {"Northwoods Consulting Partners"},
            Description              = ".NET library to expose controls to Automation",
            ProjectUrl               = new Uri("https://github.com/northwoodspd/UIA.Extensions"),
            Copyright                = "Northwoods Consulting Partners 2020",
            RequireLicenseAcceptance = false,
            Symbols                  = false,
            NoPackageAnalysis        = true,
            Files                    = new [] { new NuSpecContent{Source = dill.FullPath, Target = target} },
            OutputDirectory          = "./nuget",
            ToolTimeout              = TimeSpan.FromMinutes(1),
            Verbosity                = NuGetVerbosity.Normal,
            Tags                     = new [] { "UIA", "UIAutomation", "Automation", "WinForms", "Accessibility" },
            LicenseUrl               = new Uri("https://raw.github.com/northwoodspd/UIA.Extensions/master/LICENSE"),
            IconUrl                  = new Uri("http://www.gravatar.com/avatar/168ea36931db8b19d9e3ec1566075b2f.jpg")
        };

        NuGetPack(nuGetPackSettings);
    }
};

///
/// Publish the Nugets
///
Action nugetPush = () =>
{
    var packages = GetFiles("./nuget/*.nupkg");

    NuGetPush(packages, new NuGetPushSettings {
        Source = $"https://api.nuget.org/v3/index.json"
    });
};

Action bundleInstall = () =>
{
    IEnumerable<string> standardOutput;
    IEnumerable<string> standardError;
    
    var exitCode = StartProcess(bundleLocation(), new ProcessSettings
    { 
        WorkingDirectory = "../", 
        RedirectStandardOutput = true, 
        RedirectStandardError = true 
    }, out standardOutput, out standardError);
    
    Information(string.Join(Environment.NewLine, standardOutput));
    
    if(exitCode != 0)
    {
        Error(string.Join(Environment.NewLine, standardError));
        throw new Exception("Unable to install bundle packages");
    }
};

Action runSpecs = () =>
{
    IEnumerable<string> standardOutput;
    IEnumerable<string> standardError;
    
    var exitCode = StartProcess(bundleLocation(), new ProcessSettings
    { 
        Arguments = "exec rake",
        WorkingDirectory = "../", 
        RedirectStandardOutput = true, 
        RedirectStandardError = true 
    }, out standardOutput, out standardError);
    
    Information(string.Join(Environment.NewLine, standardOutput));
    
    if(exitCode != 0)
    {
        Error(string.Join(Environment.NewLine, standardError));
        throw new Exception("Error running specs");
    }
};

Func<string> bundleLocation = () =>
{
    IEnumerable<string> standardOutput;
    IEnumerable<string> standardError;
    
    var exitCode = StartProcess("which", new ProcessSettings
    { 
        Arguments = "bundle.bat",
        RedirectStandardOutput = true, 
        RedirectStandardError = true 
    }, out standardOutput, out standardError);
    
    string location = string.Join("", standardOutput);
        
    if(exitCode != 0 || String.IsNullOrWhiteSpace(location))
    {
        Error(string.Join(Environment.NewLine, standardError));
        throw new Exception($"Unable to find bundle {location}");
    }
    
    return location;
};

///
/// //////////////////////////////////////////////////
///
///                  CAKE TASKS
///
/// //////////////////////////////////////////////////
///

///
/// Cleans the obj folders
///
Task("Clean-Obj")
    .Does(() => cleanAllDirectories("obj"));

///
/// Cleans the bin folders
///
Task("Clean-Bin")
    .Does(() => cleanAllDirectories("bin"));

///
/// Cleans the bin and obj folders
///
Task("Clean")
    .IsDependentOn("Clean-Obj")
    .IsDependentOn("Clean-Bin");

///
/// Restores Nugets from configured artifact repository(ies)
///
Task("Restore")
    .Does(nugetRestore)
    .Does(bundleInstall);

///
/// Restores Nugets from configured artifact repository(ies)
///
Task("AssemblyVersion")
    .Does(setAssemblyVersion);

///
/// Builds the Solution
///
Task("Build")
    .IsDependentOn("Restore")
    .IsDependentOn("AssemblyVersion")
    .Does(() =>
    {
        buildSolution();
    });

///
/// Runs the Unit Tests
///
Task("Test")
    .IsDependentOn("Build")
    .Does(() => {
        runSpecs();
        executeTests(testAssemblyPattern, "unit", dotCoversettings => { }, 60000, TimeSpan.FromMinutes(5));
    });

///
/// Runs the Integration Tests
///
Task("Integration-Test")
    .IsDependentOn("Build")
    .Does(() => executeNUnitTests(integrationTestAssemblyPattern, 120000, TimeSpan.FromMinutes(5)));

///
/// Runs Restore, Build, Test, Integration Test
///
Task("Check")
    .IsDependentOn("Test")
    .IsDependentOn("Integration-Test");

///
/// Runs Clean, Restore, Build, Test, Integration Test
///
Task("Rebuild")
    .IsDependentOn("Clean")
    .IsDependentOn("Build");

///
/// Makes the nupkg files
///
Task("Pack")
    .IsDependentOn("AssemblyVersion")
    .Does(nugetPack);

///
/// Push the nupkg files to the nuget repo
///
Task("Push")
    .Does(nugetPush);

///
/// DO both
///
Task("Pack+Push")
    .IsDependentOn("AssemblyVersion")
    .Does(nugetPack)
    .Does(nugetPush);

Task("All")
    .IsDependentOn("Check")
    .IsDependentOn("Pack+Push");

Task("Default")
    .IsDependentOn("Check");

RunTarget(target);
