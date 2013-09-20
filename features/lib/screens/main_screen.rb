class MainScreen
  include Mohawk, RAutomation::Adapter::MsUia
  window :title => /UIA\.Fluent/

  control(:panel, :id => 'basicPanel')
  control(:calendar, :id => 'monthCalendar')

  button(:add_rows, :value => 'Add Row')
  button(:delete_last, :value => 'Delete Last')
  button(:update_headers, :value => 'Update Headers')

  table(:the_grid, :id => 'dataGridView')
  text(:how_many, :id => 'howManyToAdd')

  spinner(:range_value, :id => 'numericUpDown')
end