When(/^the exposed value control is set to "(.*?)"$/) do |value|
  on(MainScreen).calendar = value
end

Then(/^the current value is known to be "(.*?)"$/) do |expected_value|
  on(MainScreen).calendar_value.should eq(expected_value)
end