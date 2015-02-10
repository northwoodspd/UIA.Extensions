require 'spec_helper'

describe 'table' do
  Given(:screen) { on(MainScreen) }

  def all_items
    screen.the_grid.map { |r| [r.firstname, r.lastname, r.age] }
  end

  context('table count') do
    When { screen.add_grid_items 10 }
    Then { screen.the_grid.count == 10 }
  end

  context 'headers' do
    Then { screen.the_grid_headers == ['FirstName', 'LastName', 'Age'] }
  end

  context 'grid item' do
    context 'values' do
      When { screen.add_grid_items 3 }
      Then { all_items == [
          ['FirstName1', 'LastName1', '1'],
          ['FirstName2', 'LastName2', '2'],
          ['FirstName3', 'LastName3', '3'],
      ] }
    end

    context 'locations' do
      Given(:cell_1_1) do
        screen.add_grid_items 3
        screen.the_grid_view.element.row_at(1).items[1]
      end
      When { cell_1_1.click_center }
      Then { expect(screen.the_grid[1]).to be_selected }
    end
  end

  context 'changing row selections' do
    Given(:four_rows) { screen.add_grid_items 4 }
    Given(:selected_rows) do
      screen.the_grid.count.times.select { |row| screen.the_grid[row].selected? }
    end

    def select_row(*rows)
      rows.each { |row| screen.add_the_grid row }
    end

    def clear_row(*rows)
      rows.each { |row| screen.clear_the_grid row }
    end

    context 'selecting' do
      Given { four_rows }
      When { screen.select_the_grid age: 3 }
      Then { expect(screen.the_grid[2]).to be_selected }
    end

    context 'selecting multiple' do
      Given { four_rows }
      When { select_row 2, 3 }
      Then { selected_rows == [0, 2, 3] }
    end

    context 'clearing' do
      Given { four_rows; 4.times { |n| screen.add_the_grid n } }
      When { clear_row 0, 3 }
      Then { selected_rows == [1, 2] }
    end

    context 'changing rows' do
      Given(:original) { screen.add_grid_items 1; all_items }
      Given(:more_rows) { screen.add_grid_items 2; all_items }

      Then { original == [
          ['FirstName1', 'LastName1', '1']
      ] }
      And { more_rows == [
          ['FirstName1', 'LastName1', '1'],
          ['FirstName1', 'LastName1', '1'],
          ['FirstName2', 'LastName2', '2'],
      ] }
    end

    context 'changing headers' do
      Given { screen.add_grid_items 1 }
      When { screen.update_headers }
      Then { screen.the_grid_headers == ['FirstName Updated', 'LastName Updated', 'Age Updated'] }
    end

    context 'hiding headers' do
      Given { screen.add_grid_items 1 }
      When { screen.hide_headers }
      Then { screen.the_grid_headers == ['Age'] }
    end
  end
end