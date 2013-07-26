class MainScreen
  include Mohawk, RAutomation::Adapter::MsUia
  window :title => /UIA\.Fluent/

  control(:panel, :id => 'basicPanel')
  control(:calendar, :id => 'monthCalendar')
end