require 'spec_helper'

describe 'invoking' do
  Given(:screen) { on(MainScreen) }

  When { screen.invokable_control }
  Then { screen.status == 'Foos have been pitied!'}
end