Feature: User accounts
	In order to interact with the site
	As a member of the public
	I want to register and manage my user account

Scenario: See my account name in the page header
	Given I have registered as a user called "Steve"
	  And I am logged in as "Steve"
	When I go to the homepage
	Then the page header should display "Welcome Steve!"
