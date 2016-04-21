Feature: Testing the example Api
Background: 
	Given I am using the base url http://www.thomas-bayer.com/

@ApiTest
Scenario: When calling api
	Given I make a GET request with url parameters for /sqlrest/CUSTOMER
	| param1 | param2 | param3 |
	| 1      | 1      | 1      |
	And I supply the header accept with value application/json
	And I supply the header content-type with value application/json
	When I call the api
	Then the api should return a response
	And the status code is 200