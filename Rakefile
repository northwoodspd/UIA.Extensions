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
end

desc 'Package for NuGet'
task :package => :build do
  puts `nuget pack src/UIA.Extensions/UIA.Extensions.csproj -Prop Configuration=Release`
end
