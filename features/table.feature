Feature: Automating table controls

  Scenario: Getting the correct row count
    When we add "10" rows to the table
    Then then the number of rows in the table should be "10"

  Scenario: Getting headers
    Then the table headers should be "FirstName, LastName, Age"

  Scenario: Getting grid items
    When we add "5" rows to the table
    Then the table should look like this:
      | firstname  | lastname  | age |
      | FirstName1 | LastName1 | 1   |
      | FirstName2 | LastName2 | 2   |
      | FirstName3 | LastName3 | 3   |
      | FirstName4 | LastName4 | 4   |
      | FirstName5 | LastName5 | 5   |

  Scenario: Selecting grid items
    Given we add "4" rows to the table
    When we select the item with the "age" of "3"
    Then the row at index "2" is selected

  Scenario: Selecting multiple grid items
    Given we add "4" rows to the table
    When we select the items at indexes "2, 3"
    Then the rows at index "2, 3" are selected

  Scenario: Clearing items in the grid
    Given we add "4" selected rows to the table
    When we clear the items at indexes "0, 3"
    Then only rows at indexes "1, 2" are selected

  Scenario: Changing Grid Items
    When we add "1" rows to the table
    Then the table should look like this:
      | firstname  | lastname  | age |
      | FirstName1 | LastName1 | 1   |
    When we add "2" more rows to the table
    Then the table should look like this:
      | firstname  | lastname  | age |
      | FirstName1 | LastName1 | 1   |
      | FirstName1 | LastName1 | 1   |
      | FirstName2 | LastName2 | 2   |

  Scenario: Updating header items
    Given we add "1" rows to the table
    When we update the headers
    Then the table should look like this:
      | firstname_updated | lastname_updated | age_updated |
      | FirstName1        | LastName1        | 1           |
