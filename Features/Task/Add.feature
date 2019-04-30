Feature: Add Task
    Scenario Outline: Add Task 
    When user tries to add a task with <name>
    Then user should see <message>
    
    Examples:
    | name  | message           |
    | space | invalid task name |
    | ab12  | task is added successfully |
    | !@#$% | invalid task name |


    Scenario Outline: Add Task failed with unexpected error
    When user tries to add a task
    And unexpected error happens
    Then user should see 'unexpected error. retry later.'


