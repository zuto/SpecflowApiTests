@UseTestServer
Feature: Testing an example Api
Background: 
	Given I am running the test server
	And I am using the base url from httpClient

Scenario: When calling api health endpoint
    Given I make a GET request for /health
    When I call the api
    Then the api should return a response
    And the status code is 200

Scenario: When calling api and returning some json
    Given I make a GET request for /json
    When I call the api
    Then the api should return a response
    And the status code is 200
	And the api response should have content as string {"Value1":1,"ValueHello":"Hello"}

Scenario: When calling api and returning some json2
    Given I make a GET request for /json
    When I call the api
    Then the api should return a response
    And the status code is 200
	And the api response should have content as json with
	| Value1 | ValueHello | 
	| 1      | Hello      | 