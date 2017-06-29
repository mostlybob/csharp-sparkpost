Feature: Transmissions

Background:
	Given my api key is 'yyy'

Scenario: Sending a regular email
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission content is
	| Subject    | Html                 |
	| Test Email | this is a test email |
	When I send the transmission
	Then it should return a 200

Scenario: Sending a regular email with an attachment
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission has a text file attachment
	And the transmission content is
	| Subject                       | Html                 |
	| Test Email with an attachment | this is a test email |
	When I send the transmission
	Then it should return a 200

Scenario: Sending a template email with an attachment, which will be ignored and no attachment will be included
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission has a text file attachment
	And the transmission template id is set to 'my-first-email'
	When I send the transmission
	Then it should return a 200

Scenario: Using CC/BCC with one direct recipient
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission is meant to be CCd to 'darrencauthon@gmail.com'
	And the transmission is meant to be BCCd to 'darrencauthon@yahoo.com'
	And the transmission content is
	| Subject                                  | Html                 |
	| Test Email With CC and BCC (1 recipient) | this is a test email |
	When I send the transmission
	Then it should return a 200

Scenario: Using CC/BCC with two direct recipients
	Given I have a new transmission
	And the transmission is meant to be sent from 'darren@cauthon.com'
	And the transmission is meant to be sent to 'darrencauthon@hotmail.com'
	And the transmission is meant to be sent to 'darren@cauthon.com'
	And the transmission is meant to be CCd to 'darrencauthon@gmail.com'
	And the transmission is meant to be BCCd to 'darrencauthon@yahoo.com'
	And the transmission content is
	| Subject                                   | Html                 |
	| Test Email With CC and BCC (2 recipients) | this is a test email |
	When I send the transmission
	Then it should return a 200