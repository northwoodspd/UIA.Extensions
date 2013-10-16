Then(/^we can invoke controls$/) do
  on(MainScreen) do |screen|
    screen.click_invokable_control
    screen.status.should eq('Foos have been pitied!')
  end
end