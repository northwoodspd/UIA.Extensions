require 'spec_helper'

describe 'valuing' do
  Given(:screen) { on(MainScreen) }

  When { screen.calendar = '9/28/1979' }
  Then { screen.calendar_value == '9/28/1979' }
end