When(/^we are on the MainScreen$/) do
  @screen = on(MainScreen)
end

Then(/^we are able to expose the following basic information from controls:$/) do |table|
  table.hashes.each do |info|
    which_property = "panel_#{info['property']}"
    @screen.send(which_property).should eq(info['value'])
  end
end