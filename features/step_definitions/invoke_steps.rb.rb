Then(/^we can invoke controls$/) do
  on(MainScreen) do |screen|
    screen.invokable_control
    screen.status.should eq('Foos have been pitied!')
  end
end