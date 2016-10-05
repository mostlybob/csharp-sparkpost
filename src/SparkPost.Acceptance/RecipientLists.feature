Feature: Recipient Lists

Background:
	Given my api key is '1234'

Scenario: Retrieving a recipient list
	Given I do not have a recipient list of id 'test-name'
	And I have a new recipient list as
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |
	When I retrieve the "my-list" recipient list
	Then it should return a 200

Scenario: Creating a recipient list
	Given I do not have a recipient list of id 'test-name'
	And I have a new recipient list as
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |
	And I add 'test@test.com' to the recipient list
	When I create the recipient list
	Then it should return a 200