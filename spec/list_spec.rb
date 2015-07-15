require 'spec_helper'

describe 'lists' do
  Given(:list) { on(MainScreen).list_view }
  Given(:selection) { list.element.as(:selection) }
  Given(:current_selection) { selection.selected_items.map(&:name) }

  Then { list.control_type === :list }
  Then { list.patterns === [:selection] }

  context 'list items' do
    Then { selection.selection_items.map(&:name) === ['First Option', 'Second Option', 'Third Option'] }
  end

  context 'selection' do
    Then { selection.selection_required? === true }
    Then { selection.multi_select? === true }

    Then { current_selection === ['First Option'] }

    context 'selecting items' do
      Given(:second_option) { selection.selection_items[1] }
      When { second_option.select }
      Then { current_selection === ['Second Option'] }
    end

    context 'un-selecting items' do
      Given(:first_option) { selection.selection_items.first }
      When { first_option.remove_from_selection }
      Then { current_selection === [] }
    end
  end
end
