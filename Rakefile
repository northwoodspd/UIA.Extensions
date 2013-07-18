require 'bundler/setup'

require 'rubygems'
require 'cucumber'
require 'cucumber/rake/task'
require 'albacore'

Cucumber::Rake::Task.new(:features) do |t|
  t.profile = 'default'
end

task :default => [:build, :features]

desc 'Build UIA.Fluent'
build :build do |b|
  b.sln = 'src/UIA.Fluent.sln'
end
