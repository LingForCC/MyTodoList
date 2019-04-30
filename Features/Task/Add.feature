Feature: Add Task
    Scenario Outline: Add Task 
    When user tries to add a task with <name>
    And <unexpected error>
    Then user should see <message>
    
    Examples:
    | name  | unexpected error | message           |
    | space | no | invalid task name |
    | ab12  | no | task is added successfully |
    | !@#$% | no | invalid task name |
    | space | yes | unexpected error. retry later. |
    | cd34  | yes | unexpected error. retry later. |



