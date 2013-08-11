Feature: RangeValue pattern controls

  Scenario: Getting and setting values
    Then the spinner can be set to "57.2"

  Scenario: Incrementing and decrement the value
    When we increment the spinner "4" times
    But we decrement the spinner "2" times
    Then the spinner should be "2"
