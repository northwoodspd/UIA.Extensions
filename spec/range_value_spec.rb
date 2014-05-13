require 'spec_helper'

describe 'range valuing' do
  Given(:screen) { on(MainScreen) }

  context 'getting / setting' do
    When { screen.range_value = 57.2 }
    Then { screen.range_value == 57.2 }
  end

  context 'incrementing and decrementing' do
    Given { 4.times { screen.increment_range_value } }
    When { 2.times { screen.decrement_range_value } }

    Then { screen.range_value == 2 }

  end
end
