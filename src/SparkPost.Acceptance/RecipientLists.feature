Feature: Recipient Lists

Scenario: Retrieving things
	Given my api key is '1234'
	When I retrieve the "my-list" recipient list
	Then it should return a 200