Feature: Automating table controls

  Scenario: Getting the correct row count
    When we add "10" rows to the table
    Then then the number of rows in the table should be "10"

  Scenario: Getting headers
    Then the table headers should be "FirstName, LastName, Age"