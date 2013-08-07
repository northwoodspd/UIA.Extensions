When(/^we add "([^"]*)" rows to the table$/) do |rows_to_add|
  on(MainScreen) do |screen|
    rows_to_add.to_i.times { screen.add_row }
  end
end

Then(/^then the number of rows in the table should be "([^"]*)"$/) do |expected_rows|
  on(MainScreen).the_grid.count.should eq(expected_rows.to_i)
end

Then(/^the table headers should be "([^"]*)"$/) do |expected_headers|
  on(MainScreen).the_grid_headers.should eq(expected_headers.split(', '))
end