Feature: Testing an example Api
Background: 
	Given I am using the base url `http://example.com/example`
	Given I am using the base url from config setting `ExampleServiceUrl`
	Given I am using the base url from httpClient
	And I set default header `AuthKey` with value `1234`

Scenario: When making a get request
	Given I setup the request to GET for resource `/`
    When I send the request
    Then I should receive a response
	And I should have a status code of 200
	And I should have a body matching `"Healthy"`

Scenario: When making a post request
    Given I setup the request to POST for resource `/example?somethingelse=1`
    And I set header `AuthKey` with value `1234`
	And I set the request content with Json
	  """
	  {
		"SomeContent": true
	  }
	  """
	And I set the request content as `application/xml`
	  """
	  <root></root>
	  """
    When I send the request
    Then I should receive a response
    And I should have status code that is a success code
	And I should have status code that is not a success code
	And I should have a body matching
	  """
	  {
		"SomeContent": true
	  }
	  """
	And I should have header `Location` with value `/Somewhere`
	And I should have content header `Length` with value `1146463`
	And I should have a content type of `application/json`
