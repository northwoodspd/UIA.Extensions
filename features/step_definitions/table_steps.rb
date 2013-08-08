When(/^we add "([^"]*)" rows to the table$/) do |rows_to_add|
  on(MainScreen) do |screen|
    screen.how_many = rows_to_add
    screen.add_rows
  end
end

Then(/^then the number of rows in the table should be "([^"]*)"$/) do |expected_rows|
  on(MainScreen).the_grid.count.should eq(expected_rows.to_i)
end

Then(/^the table headers should be "([^"]*)"$/) do |expected_headers|
  on(MainScreen).the_grid_headers.should eq(expected_headers.split(', '))
end

Then(/^the table should look like this:$/) do |table_info|
  on(MainScreen) do |screen|
    table_info.hashes.each_with_index do |row_info, row|
      the_row = screen.the_grid[row]
      actual_values = row_info.keys.map(&the_row.method(:send))
      actual_values.should eq(row_info.values)
    end
  end
end