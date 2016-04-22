# SpecFlowApiTests

A set of bindings to help to test an api, without having to implement back end code by encapsulating common testing patterns


###Example usage

```
Feature: Testing the example Api
Background: 
	Given I am using the base url http://example.api.com

@ApiTest
Scenario: When calling api
	Given I make a GET request with url parameters
	| param1 | param2 | param3 |
	| 1      | 1      | 1      |
	When I call the api
	Then the api should return a response
	And the status code is 200
	And the status code a success code
	And the api response should have a content type of 
```

###Binding the Specflow.Api tests dll to your solution

Add the following into the specflow config
```
<specFlow>
  <stepAssemblies>
    <stepAssembly assembly="SpecFlow.ApiTests" />
  </stepAssemblies>
</specFlow>
```
