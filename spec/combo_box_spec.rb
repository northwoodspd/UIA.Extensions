require 'spec_helper'

describe 'combo boxes' do
  Given(:combo) { on(MainScreen).combo_view }

  Then { combo.control_type === :combo_box }
  Then { combo.patterns === [:selection] }

  context 'selection' do
    Given(:selection) { combo.element.as(:selection) }

    Then { selection.selection_required? === true }
  end
end
