class MainScreen
  include Mohawk
  window title: /UIA\.Fluent/

  control(:calendar, :id => 'monthCalendar')
  control(:panel, :id => 'basicPanel')

  button(:invokable_control, id: 'pictureBox1')

  label(:status, id: 'StatusBar.Pane0')

  spinner(:range_value, :id => 'numericUpDown')
end