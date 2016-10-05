Feature: Transmissions

Background:
	Given my api key is 'da72d0e0bf7b26e4e931137375a5f58f7b782445'

Scenario: Sending a regular email
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission content is
	| Subject    | Html                 |
	| Test Email | this is a test email |
	When I send the transmission
	Then it should return a 200