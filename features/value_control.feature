Feature:  Exposing controls with values

  Scenario: Getting and setting values for a control
    When the exposed value control is set to "9/28/1979"
    Then the current value is known to be "9/28/1979"