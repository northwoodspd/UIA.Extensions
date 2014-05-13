Feature:  Exposing basic automation information from controls

  Scenario: Wrapping basic automation information from controls
    When we are on the MainScreen
    Then we are able to expose the following basic information from controls:
      | property     | value             |
      | control_name | Custom Panel Name |
      | control_type | custom            |

  Scenario: Nesting children
    Then we can add children automation providers
