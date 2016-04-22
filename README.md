# SpecFlowApiTests

A set of bindings to help to test an api, without having to implement back end code by encapsulating common testing patterns


###Example usage

```
Feature: Testing the example Api
Background:
	Given I am using the base url from config setting ApiBaseUrl

@ApiTest
Scenario: When calling api health endpoint
	Given I make a GET request for /health
	When I call the api
	Then the api should return a response
	And the status code is 200
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


The following language is supported
```
#Given

I am using the base url for {baseUrl}

I am using the base url from config setting {configKey}

I make a {verb} request for {route}

I make a {verb} request with url parameters for {path}
| parameterName   | parameterName   |
| parameterValue  | parameterValue  |
        
I supply a request header {headerName} with value {headerValue}
        
I set the request content type with StringContent to {stringValue}

I set the request content type with MultipartContent and files
| File1    | File2    |
| Filename | Filename |


#When 

I call the api


#Then

the api should return a response

the status code is {statusCode}

the status code a success code 
        
the status code is not a success code
        
the api response should have a content type of {contentType}

the api response should have content as string {stringContent}
```
