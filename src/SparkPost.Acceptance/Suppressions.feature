Feature: Suppressions

Background:
	Given my api key is 'yyy'

Scenario: Adding an email to the suppressions list
	When I add 'testing@cauthon.com' to my suppressions list
	Then it should return a 200
	And 'testing@cauthon.com' should be on my suppressions list