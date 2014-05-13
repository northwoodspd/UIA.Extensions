require 'childprocess'

include Mohawk::Navigation

Mohawk.app_path = File.join(File.dirname(__FILE__), '../../src/UIA.Extensions.TestApplication/bin/Debug/UIA.Extensions.TestApplication.exe')

Before do
  Mohawk.start
end

After do
  Mohawk.stop
end
