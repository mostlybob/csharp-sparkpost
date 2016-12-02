Feature: Metrics

Background:
	Given my api key is 'yyy'

Scenario: Sending a regular email
	When I query my deliverability
	Then it should return a 200