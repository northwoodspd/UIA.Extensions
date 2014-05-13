require 'spec_helper'

describe 'controls' do
  Given(:panel) { on(MainScreen).panel_view }

  context 'properties' do
    Then { panel.control_name == 'Custom Panel Name' }
    Then { panel.control_type == :custom }
  end

  context 'adding children' do
    Given(:invokable) { on(MainScreen).invokable_control_view.element }
    Then { invokable.children.map(&:name) == ['First Child', 'Second Child'] }
  end
end