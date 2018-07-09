Feature: ManagerLogin
	In order to assign cases
	As a Manager
	I want to login to IWMS system

@smoke
Scenario: Manager logs in successfully
Given I am on the login page
And I enter Username and password for a manager
| username | password |
| SophiaAss | infy4321 |
When I click on Login button
Then I should be taken to team dashboard
And Manager name 'Sophia' should be displayed on the Team Dashboard
