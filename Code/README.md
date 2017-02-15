# SpecFlowApiTests

A set of bindings to help rest something that can be called using HTTP requests, without having to implement back end code

### Setup
Binding the Specflow.Api tests dll to your solution
Add the following into the specflow config
```xml
<specFlow>
  <stepAssemblies>
    <stepAssembly assembly="SpecFlow.ApiTests" />
  </stepAssemblies>
</specFlow>
```

### How to use

Many helpers have been created to be able to assert and setup most functionality that will be needed for api testing

| Tags  | |
| :------------ | :------------ |
| *TagName* | *Description* |
| @Verbose | Outputs request and response to console |

| Background Given (Initial Setup) ||
| :------------ | :------------ |
| *Helper* | *Description* |
| Given I am using the base url`` `http://example.com/example` ``| Sets all requests to point to the base url specified |
| Given I am using the base url from config setting`` `ExampleServiceUrl` ``| Sets all requests to point to the base url in the config file AppSettings |
| Given I set default header`` `AuthKey` ``with value`` `123` ``| Sets a default request header that is applied to all requests |
   
|Given (Setup)||
| :------------ | :------------ |
|Helper|Description|
|Given I setup the request to GET for resource`` `/` ``|Creates a new request message, with verb for a resource path|
|Given I setup the request to POST for resource`` `/example?somethingelse=1` ``| Creates a new request message, with verb for a resource path|
|And I set header`` `AuthKey` ``with value`` `1234` ``|Adds a header into the request with key and value|

|The following support multiline parameter content||
| :------------ | :------------ |
|And I set the request content with Json |Sets the request content with json type, allowing multiline json content|
| And I set the request content as `application/xml` | Example of setting a specific content type e.g. xml |    
- this should be supplied with multiline parameter, e.g.
```
    """
        {
            "SomeContent": true
        }
    """ 
```

|When(Action)||
| :------------ | :------------ |
|When I send the request | Sends the request|

|Then(Assertion)||
| :------------ | :------------ |
| Then I should receive a response | No functionality, but reads better|
| And I should have a status code of 200 | Asserts response status code match |
| And I should have content matching`` `"Healthy"` `` | Asserts response content matching exactly| 
| And I should have content partially matching`` `Healt"` ``| Asserts response content matching partially |
|Then I should receive a response| No functionality, but reads better|
|And I should have status code that is a success code| Asserts the response code is less than 400|
|And I should have status code that is not a success code|Asserts the response code is 400 or greater|
|And I should have content partially matching`` `"SomeContent": true` ``|Asserts response content matching partially|    
| And I should have content matching`` `{ "SomeContent": true }`	 ``| Asserts response content matching Exact|    
|And I should have header`` `Location` ``with value`` `/location` ``|Asserts response header exists matching key and value|
|And I should have content header`` `Length` ``with value`` `1234` ``| Asserts response content header exists matching key and value|
    
|The following support multiline parameter content|
| :------------ | :------------ |
|And I should have a content type of`` `application/json` ``|Asserts response content-type header matches value|            
| And I should have content matching | Asserts response content matching Exact with multiline support |
- this should be supplied with multiline parameter, e.g.
```    
    """
        {
	        "SomeContent": true 
        }
    """
```
    

### Example usage - a multistep test

```gherkin
@Verbose
Feature: Testing an example 1
Background: 
    Given I am using the base url `http://my.zuto.com/api/`

Scenario: I am testing a create, get and delete
    Given I setup the request to delete for resource `/example`
    When I send the request
    Then I should receive a response	
    Given I setup the request to POST for resource `/example`
    And I set the request content with Json
	  """
	  {
		"SomeContent": true
	  }
	  """
    When I send the request
    Then I should receive a response
    And I should have a status code of 200    
    Given I setup the request to GET for resource `/example`
    When I send the request
    Then I should receive a response
    And I should have a status code of 200    
    And I should have content partially matching `"SomeContent": true`	  
    And I should have header `Location` with value `/location`
    And I should have content header `Length` with value `1234`
    And I should have a content type of `application/json`
```

### Example usage of a health endpoint
```gherkin
@Verbose 
Feature: Testing an example Api
Background: 	
	Given I am using the base url `http://example.com/example`	
	Given I am using the base url from config setting `ExampleServiceUrl`	
	And I set default header `AuthKey` with value `1234`

Scenario: When making a get request	
	Given I setup the request to GET for resource `/health`	
    When I send the request	
    Then I should receive a response	
	And I should have a status code of 200
	And I should have content matching `"Healthy"`		
```

