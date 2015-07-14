require 'spec_helper'

describe 'combo boxes' do
  Given(:combo) { on(MainScreen).combo_view }

  Then { combo.control_type === :combo_box }
end
