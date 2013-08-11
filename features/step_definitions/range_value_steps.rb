Then(/^the spinner can be set to "([^"]*)"$/) do |value|
  on(MainScreen) do |screen|
    screen.range_value = value.to_f
    screen.range_value.should eq(value.to_f)
  end
end

When(/^we (increment|decrement) the spinner "([^"]*)" times$/) do |what, how_many|
  on(MainScreen) do |screen|
    how_many.to_i.times { screen.send("#{what}_range_value") }
  end
end

Then(/^the spinner should be "([^"]*)"$/) do |expected_value|
  on(MainScreen).range_value.should eq(expected_value.to_f)
end

