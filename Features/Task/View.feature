Feature: View Task
    Scenario Outline: View Task 
    Given user has added <addedTasks> before
    When user accesses the main page
    Then user should see <tasks>

    Examples:
    | addedTasks         | tasks              |
    | no tasks           | no tasks           |
    | running, buy books | running, buy books |
    | running            | running            |

