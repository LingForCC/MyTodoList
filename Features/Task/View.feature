Feature: View Task
    Scenario: View task if task is added successfully
    Given a task is added successfully,
    Then user should see the task in the list

    Scenario Outline: Not seeing task if task is not added successfully
    Given a task is not added because of <error>
    Then user should not see the task in the list

    Examples:
    | error  | 
    | invalid task name  |
    | unexpected error   |


