require 'bundler/setup'

require 'rubygems'
require 'rspec/core/rake_task'
require 'rspec/given'

task :default => [:build, :spec]

desc 'Build the Release UIA.Extensions'
task :build do
  Dir.chdir('src') do
    if !system('msbuild UIA.Extensions.sln -target:Rebuild -property:Configuration=Release')
      fail "Failed to build UIA.Extensions: #{$?.exitstatus}"
    end
  end
end

desc 'Package for NuGet'
task :package => [:build, :spec] do
  puts `nuget pack src/UIA.Extensions/UIA.Extensions.csproj -Prop Configuration=Release`
end

RSpec::Core::RakeTask.new(:spec) do |spec|
  spec.ruby_opts = "-I lib:spec"
  spec.pattern = 'spec/**/*_spec.rb'
  spec.rspec_opts = "--color -f documentation"
end
