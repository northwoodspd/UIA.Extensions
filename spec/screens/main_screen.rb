class MainScreen
  include Mohawk
  window title: /UIA\.Fluent/

  control(:calendar, :id => 'monthCalendar')
  control(:panel, :id => 'basicPanel')

  button(:invokable_control, id: 'pictureBox1')
  button(:add_rows, :value => 'Add Row')
  button(:update_headers, :value => 'Update Headers')
  button(:toggle_row, :value => 'Toggle Row')

  label(:status_strip, id: 'statusStrip1')

  table(:the_grid, :id => 'dataGridView')

  spinner(:range_value, :id => 'numericUpDown')

  text(:how_many, :id => 'howManyToAdd')

  select_list(:list, id: 'fakeCombo')

  def add_grid_items(how_many)
    self.how_many = how_many.to_s
    add_rows
  end

  def status
    self.status_strip_view.children[0].name
  end
end
