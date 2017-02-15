#Outputs request and response to console
@Verbose 
Feature: Testing an example Api
#Background: 
#	#Sets all requests to point to the base url specified
#	Given I am using the base url `http://example.com/example`
#	#Sets all requests to point to the base url in the config file (AppSettings)
#	Given I am using the base url from config setting `ExampleServiceUrl`
#	#Sets a default request header that is applied to all requests
#	And I set default header `AuthKey` with value `1234`
#
#Scenario: When making a get request
#	#Creates a new request message, with verb for a resource path
#	Given I setup the request to GET for resource `/`
#	#Sends the request
#    When I send the request
#	#No functionality, but reads better
#    Then I should receive a response
#	#Asserts response status code match
#	And I should have a status code of 200
#	#Asserts response content matching exactly
#	And I should have content matching `"Healthy"`
#	#Asserts response content matching partially
#	And I should have content partially matching `Healt"`
#
#Scenario: When making a post request
#	#Creates a new request message, with verb for a resource path
#    Given I setup the request to POST for resource `/example?somethingelse=1`
#	#Adds a header into the request with key and value
#    And I set header `AuthKey` with value `1234`
#	#Sets the request content with json type, allowing multiline json content
#	And I set the request content with Json
#	  """
#	  {
#		"SomeContent": true
#	  }
#	  """
#	#Example of setting a specific content type e.g. xml
#	And I set the request content as `application/xml`
#	  """
#	  <root></root>
#	  """
#	#Sends the request
#    When I send the request
#	#No functionality, but reads better
#    Then I should receive a response
#	#Asserts the response code is less than 400
#    And I should have status code that is a success code
#	#Asserts the response code is 400 or greater
#	And I should have status code that is not a success code
#	#Asserts response content matching partially
#	And I should have content partially matching `"SomeContent": true`	  
#	#Asserts response content matching Exact
#	And I should have content matching `{ "SomeContent": true }`	  
#	#Asserts response content matching Exact with multiline support
#	And I should have content matching 
#	  """
#	    {
#			"SomeContent": true 
#		}
#	  """
#	#Asserts response header exists matching key and value
#	And I should have header `Location` with value `/location`
#	#Asserts response content header exists matching key and value
#	And I should have content header `Length` with value `1234`
#	#Asserts response content-type header matches value
#	And I should have a content type of `application/json`
