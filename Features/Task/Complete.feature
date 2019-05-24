Feature: Compelete Task
    Scenario: Complete Task
    Given user has <number of uncompleted task before completion> uncompleted tasks
    When user completes a task
    Then user should have <number of uncompleted task after completion> uncompleted tasks
    
    Examples:
    | number of uncompleted task before completion | number of uncompleted task after completion |
    | 3                                            |   2                                         |
    

    Scenario: Should not be able to complete any task
    Given user has 0 uncompleted tasks
    Then user should not be able to complete any task
