When(/^we add "([^"]*)"( more)? rows to the table$/) do |rows_to_add, moar_rows|
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
    expected_headers = table_info.hashes.map(&:keys).first.map(&:to_sym)
    screen.the_grid_headers.map(&:to_method).should eq(expected_headers)
    screen.the_grid.map(&:cells).should eq(table_info.hashes.map(&:values))
  end
end

When(/^we select the item with the "([^"]*)" of "([^"]*)"$/) do |column, value|
  on(MainScreen).select_the_grid column.to_sym => value
end

Then(/^the row at index "([^"]*)" is selected$/) do |expected_index|
  on(MainScreen).the_grid[expected_index.to_i].should be_selected
end

When(/^we update the headers$/) do
  on(MainScreen).update_headers
end