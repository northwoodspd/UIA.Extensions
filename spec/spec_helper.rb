require 'rspec/given'
require 'childprocess'
require 'uia'
require 'require_all'
require 'mohawk'

require_rel 'screens'

include Mohawk::Navigation
Mohawk.app_path = File.join(File.dirname(__FILE__), '../src/UIA.Extensions.TestApplication/bin/Debug/UIA.Extensions.TestApplication.exe')

RSpec.configure do |config|
  config.before(:each) do
    Mohawk.start
  end

  config.after(:each) do
    Mohawk.stop
  end
end
