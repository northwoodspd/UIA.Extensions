require 'bundler/setup'

require 'rubygems'
require 'cucumber'
require 'cucumber/rake/task'
require 'albacore'

Cucumber::Rake::Task.new(:features) do |t|
  t.profile = 'default'
end

task :default => [:build, :features]

desc 'Build UIA.Extensions'
build :build do |b|
  b.sln = 'src/UIA.Extensions.sln'
  b.prop :Configuration, :Debug
end

desc 'Build the Release UIA.Extensions'
build :build_release do |b|
  b.sln = 'src/UIA.Extensions.sln'
  b.prop :Configuration, :Release
end

desc 'Package for NuGet'
task :package => :build_release do
  puts `nuget pack src/UIA.Extensions/UIA.Extensions.csproj -Prop Configuration=Release`
end
