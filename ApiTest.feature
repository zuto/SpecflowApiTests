@Ignore
@UseMockServer
Feature: Testing an example Api
Background: 
	Given I am using the base url from httpClient

@ContractLocal
Scenario: When calling api health endpoint
    Given I make a GET request for /health
    When I call the api
    Then the api should return a response
    And the status code is 200