class MainScreen
  include Mohawk
  window title: /UIA\.Fluent/

  control(:calendar, :id => 'monthCalendar')
  control(:panel, :id => 'basicPanel')

  button(:invokable_control, id: 'pictureBox1')
  button(:add_rows, :value => 'Add Row')
  button(:update_headers, :value => 'Update Headers')
  button(:hide_headers, :value => 'Hide Headers')

  label(:status, id: 'StatusBar.Pane0')

  table(:the_grid, :id => 'dataGridView')

  spinner(:range_value, :id => 'numericUpDown')

  text(:how_many, :id => 'howManyToAdd')

  def add_grid_items(how_many)
    self.how_many = how_many.to_s
    add_rows
  end
end