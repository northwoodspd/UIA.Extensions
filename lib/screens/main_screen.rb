class MainScreen
  include Mohawk, RAutomation::Adapter::MsUia
  window :title => /UIA\.Fluent/

  control(:panel, :id => 'basicPanel')
  control(:calendar, :id => 'monthCalendar')

  button(:add_row, :value => 'Add Row')

  table(:the_grid, :id => 'dataGridView')
end