class MainScreen
  include Mohawk
  window title: /UIA\.Fluent/

  button(:invokable_control, id: 'pictureBox1')
  label(:status, id: 'StatusBar.Pane0')
  control(:calendar, :id => 'monthCalendar')
end