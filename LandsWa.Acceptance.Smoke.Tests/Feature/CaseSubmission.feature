Feature: CaseSubmission
	In order to create new cases
	As an Officer
	I want to submit new cases for applicants

@E2E
Scenario: Verify that a case can be submitted successfully
Given a web browser is at IWMS login page
When the officer enters username "BenAss" and password "infy4321" to login
And searches for an applicant "Ravi"
And creates a new case for this applicant with the following information
| key                           | value            |
| requestCategory               | Easement         |
| requestDescription            | Automation Tests |
| lga                           | Joondalup        |
| suburb                        | South Perth      | 
| methodOfContact               | Email            |
| sendOtherDocumentsToApplicant | No               |
Then a new case will be created on team dashboard

