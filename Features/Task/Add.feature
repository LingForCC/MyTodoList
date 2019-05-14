Feature: Add Task
    Scenario Outline: Add Task 
    When user tries to add a task with <name>
    Then user should see <action>
    Then user should see 
    
    Examples:
    | name  | action           |
    | space | see the alert of 'invalid task name' |
    | ab12  | the task in the list |
    | !@#$% | see the alert of 'invalid task name' |


    Scenario Outline: Add Task failed with unexpected error
    When user tries to add a task
    And unexpected error happens
    Then user should see 'unexpected error. retry later.'


