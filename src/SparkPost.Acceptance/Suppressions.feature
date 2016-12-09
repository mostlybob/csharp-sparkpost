Feature: Suppressions

Background:
	Given my api key is 'yyy'

Scenario: Adding an email to the suppressions list
	Given I have a random email address ending in '@cauthon.com'
	When I add my random email address a to my suppressions list
	Then it should return a 200
	And my random email address should be on my suppressions list