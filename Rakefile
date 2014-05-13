require 'bundler/setup'

require 'rubygems'
require 'cucumber'
require 'cucumber/rake/task'
require 'albacore'
require 'rspec/core/rake_task'
require 'rspec/given'

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

RSpec::Core::RakeTask.new(:spec) do |spec|
  spec.ruby_opts = "-I lib:spec"
  spec.pattern = 'spec/**/*_spec.rb'
  spec.rspec_opts = "--color -f documentation"
end
