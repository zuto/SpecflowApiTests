Feature: ApiTest
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: When calling api
	Given I make a GET request with url parameters
	| param1 | param2 | param3 |
	| 1      | 1      | 1      |
	When I call the api
	Then the api should return a response
	And the status code is 200
	And the api response should have a content type of 
