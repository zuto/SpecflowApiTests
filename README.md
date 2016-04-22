# SpecFlowApiTests

A set of bindings to help to test an api, without having to implement back end code by encapsulating common testing patterns


###Example usage

```
Feature: Testing an example Api
Background: 
	Given I am using the base url from config setting ApiBaseUrl

@ApiTest
Scenario: When calling api health endpoint	
	Given I make a GET request for /health	
	When I call the api
	Then the api should return a response
	And the status code is 200
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
