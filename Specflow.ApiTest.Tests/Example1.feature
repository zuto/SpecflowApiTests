Feature: Testing an example 1
Background: 
	Given I am using the base url `http://my.zuto.com/api/`

Scenario: When making a get request
	Given I setup the request to GET for resource `/`
	When I send the request
    Then I should receive a response
	And I should have a status code of 200
	Given I setup the request to GET for resource `/nosuchcontent`
	When I send the request
    Then I should receive a response
	And I should have a status code of 404
	Given I setup the request to POST for resource `/`
	When I send the request
    Then I should receive a response
	And I should have a status code of 200