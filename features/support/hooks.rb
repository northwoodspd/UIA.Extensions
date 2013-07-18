require 'childprocess'

include Mohawk::Navigation

def app_path
  File.join(File.dirname(__FILE__), '../../src/UIA.Fluent.TestApplication/bin/Debug/UIA.Fluent.TestApplication.exe')
end

Before do
  @app = ChildProcess.build app_path
  @app.start
  RAutomation::WaitHelper.wait_until { RAutomation::Window.new(:pid => @app.pid).present? }
end

After do
  @app.stop if @app
end
