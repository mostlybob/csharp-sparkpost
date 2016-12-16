Feature: Message Events

Background:
	Given my api key is '60924876b5df842cbec2489917388149935cd09a'

Scenario: Samples
	When I ask for samples of 'send'
	Then it should return a 200