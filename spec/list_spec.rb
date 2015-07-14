require 'spec_helper'

describe 'lists' do
  Given(:list) { on(MainScreen).list_view }

  Then { list.control_type === :list }
  Then { list.patterns === [:selection] }

  context 'selection' do
    Given(:selection) { list.element.as(:selection) }

    Then { selection.selection_required? === true }
    Then { selection.multi_select? === true }
  end
end
