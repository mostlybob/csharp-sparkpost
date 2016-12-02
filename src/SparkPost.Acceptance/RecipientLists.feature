Feature: Recipient Lists

Background:
	Given my api key is 'yyy'

Scenario: Retrieving a recipient list
	Given I do not have a recipient list of id 'test-name'
	And I have a new recipient list as
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |
	And I add 'test@test.com' to the recipient list
	And I add 'test2@test.com' to the recipient list
	When I create the recipient list
	When I retrieve the "test-name" recipient list
	Then it should return a 200
	And it should have the following recipients
	| Email          |
	| test@test.com  |
	| test2@test.com |
	And it should have the following recipient list values
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |

Scenario: Creating a recipient list
	Given I do not have a recipient list of id 'test-name'
	And I have a new recipient list as
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |
	And I add 'test@test.com' to the recipient list
	When I create the recipient list
	Then it should return a 200
	When I retrieve the "test-name" recipient list
	Then it should have the following recipients
	| Email          |
	| test@test.com  |

Scenario: Updating a recipient list
	Given I do not have a recipient list of id 'test-name'
	And I have a new recipient list as
	| Id        | Name      | Description      |
	| test-name | Test Name | Test Description |
	And I add 'test@test.com' to the recipient list
	And I add 'test2@test.com' to the recipient list
	When I create the recipient list
	Given I have a new recipient list as
	| Id        | Name            | Description             |
	| test-name | Test Name Again | Test Description Second |
	And I clear the recipients on the recipient list
	And I add 'test1@test.com' to the recipient list
	And I add 'test3@test.com' to the recipient list
	When I update the recipient list
	Then it should return a 200
	When I retrieve the "test-name" recipient list
	Then it should return a 200
	And it should have the following recipient list values
	| Id        | Name            | Description             |
	| test-name | Test Name Again | Test Description Second |
	And it should have the following recipients
	| Email          |
	| test1@test.com |
	| test3@test.com |