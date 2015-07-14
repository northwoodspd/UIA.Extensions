require 'spec_helper'

describe 'lists' do
  Given(:list) { on(MainScreen).list_view }
  Given(:selection) { list.element.as(:selection) }

  Then { list.control_type === :list }
  Then { list.patterns === [:selection] }

  context 'list items' do
    Then { selection.selection_items.map(&:name) === ['First Option', 'Second Option', 'Third Option'] }
  end

  context 'selection' do
    Then { selection.selection_required? === true }
    Then { selection.multi_select? === true }
  end
end
