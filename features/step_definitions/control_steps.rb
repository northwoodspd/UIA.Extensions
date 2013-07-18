When(/^we are on the MainScreen$/) do
  @screen = on(MainScreen)
end

Then(/^we are able to expose the following basic information from controls:$/) do |table|
  table.hashes.each do |info|
    @screen.panel_view.send(info['property']).to_s.should eq(info['value'])
  end
end