Feature: Testing the example Api
Background: 
	Given I am using the base url from config setting ApiBaseUrl
#
#@ApiTest
#Scenario: When calling api health endpoint	
#	Given I make a GET request for /health	
#	When I call the api
#	Then the api should return a response
#	And the status code is 200
#
#@ApiTest
#Scenario: When calling api post paperwork endpoint for selfie
#	Given I make a POST request for /applications/0/paperwork	
#	And I set the request content type with MultipartContent and files
#	| File1                 |
#	| C:\Code\SpecFlowApiTests\testfile.txt |
#	When I call the api
#	Then the api should return a response
#	And the status code is 200