Feature: Metrics

Background:
	Given my api key is 'yyyy'

Scenario: Sending a regular email
	When I query my deliverability for count_accepted
	Then it should return a 200
	And it should return some metrics count