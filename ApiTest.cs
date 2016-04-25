﻿using NUnit.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TechTalk.SpecFlow;

namespace Specflow.ApiTests
{
    [Binding]
    internal class SpecFlowApiSpecficTests
    {
        [BeforeScenario("UseTestServer")]
        public void BeforeUseTestServer()
        {
        }

        [AfterScenario("UseTestServer")]
        public void AfterUseTestServer()
        {
        }
    }

    [Binding]
    public class SpecFlowApiTests
    {
        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }
        private static string _urlParameters;
        private static string _urlRoute;

        public static void SwapOutHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        [BeforeScenario("ApiTest")]
        public void BeforeApiTests()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();

            Console.WriteLine($"Using HttpClient with base url: {HttpClient.BaseAddress}");
        }

        [Given(@"I am using the base url from httpClient")]
        public void GivenIAmUsingTheBaseUrlFromHttpClient()
        {
            //Make sure HttpClient has been swapped out with specific instance
            HttpRequestMessage = new HttpRequestMessage();
        }
        [Given(@"I am using the base url for (.*)")]
        public void GivenIAmUsingTheBaseUrl(string baseUrl)
        {
            HttpClient.BaseAddress = new Uri(baseUrl);
            HttpRequestMessage = new HttpRequestMessage();
        }
        [Given(@"I am using the base url from config setting (.*)")]
        public void GivenIAmUsingTheBaseUrlFromConfigSetting(string configKey)
        {
            HttpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings[configKey]);
            HttpRequestMessage = new HttpRequestMessage();
        }


        [Given(@"I make a (.*) request with url parameters for (.*)")]
        public void GivenIMakeARequestWithUrlParametersFor(string verb, string path, Table table)
        {
            HttpRequestMessage.Method = new HttpMethod(verb);
            var @params = "?";
            foreach (var keyValuePair in table.Rows[0])
            {
                @params += keyValuePair.Key + "=" + keyValuePair.Value + "&";
            }
            @params = @params.Remove(@params.Length - 1);
            _urlParameters = @params;
            _urlRoute = path;
        }
        [Given(@"I supply a request header (.*) with value (.*)")]
        public void GivenISupplyARequestHeaderWithValue(string headerName, string headerValue)
        {
            HttpRequestMessage.Headers.Add(headerName, headerValue);
        }
        [Given(@"I supply a request content header (.*) with value (.*)")]
        public void GivenISupplyARequestContentHeaderWithValue(string headerName, string headerValue)
        {
            HttpRequestMessage.Content.Headers.Add(headerName, headerValue);
        }

        [Given(@"I set the request content type with StringContent to (.*)")]
        public void GivenISetTheRequestContentTypeWithStringContentToValue(string value)
        {
            HttpRequestMessage.Content = new StringContent(value);
        }

        [Given(@"I set the request content type with MultipartContent and files")]
        public void GivenISetTheRequestContentTypeToSomething(Table files)
        {
            HttpRequestMessage.Content = new MultipartContent();
            foreach (var file in files.Rows[0])
            {
                var path = file.Value;
                var content = new ByteArrayContent(File.ReadAllBytes(path));
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("File") { FileName = path };
                ((MultipartContent)HttpRequestMessage.Content).Add(content);
            }
        }

        [Given(@"I make a (.*) request for (.*)")]
        public void GivenIMakeARequestWithUrlParameters(string verb, string path)
        {
            HttpRequestMessage.Method = new HttpMethod(verb);
            _urlRoute = path;
        }


        [When(@"I call the api")]
        public void WhenICallTheApi()
        {
            HttpRequestMessage.RequestUri = new Uri(_urlRoute + _urlParameters, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
            Console.WriteLine(HttpRequestMessage);
            Console.WriteLine(HttpResponseMessage);

        }
        [Then(@"the api should return a response")]
        public void ThenTheApiShouldReturnAResponse()
        {
            //nothing to do yet
        }
        [Then(@"the status code is (.*)")]
        public void ThenTheStatusCodeIs(int statusCode)
        {
            Assert.That((int)HttpResponseMessage.StatusCode, Is.EqualTo(statusCode));
        }
        [Then(@"the status code a success code")]
        public void ThenTheStatusCodeIsAStatusCode()
        {
            Assert.That(HttpResponseMessage.IsSuccessStatusCode);
        }
        [Then(@"the status code is not a success code")]
        public void ThenTheStatusCodeIsNotAStatusCode()
        {
            Assert.That(!HttpResponseMessage.IsSuccessStatusCode);
        }
        [Then(@"the api response should have a content type of (.*)")]
        public void ThenTheApiResponseShouldHaveAContentTypeOf(string contentType)
        {
            Assert.That(HttpResponseMessage.Content.Headers.ContentType.MediaType, Is.EqualTo(contentType));
        }

        [Then(@"the api response should have content as string (.*)")]
        public void ThenTheApiResponseShouldHaveContentAsString(string stringContent)
        {
            Assert.That(HttpResponseMessage.Content.ReadAsStringAsync().Result, Is.EqualTo(stringContent));
        }

        [Then(@"the api response should have content as json with")]
        public void ThenTheApiResponseShouldHaveContentAsJson(Table table)
        {
            var result = HttpResponseMessage.Content.ReadAsStringAsync().Result;
            JObject json = JObject.Parse(result);
            foreach (var keyValuePair in table.Rows.ElementAt(0))
            {
                Assert.That(json.SelectTokens(keyValuePair.Key).Count(), Is.GreaterThan(0), "Could not find json token " + keyValuePair.Key);
                Assert.That(json.SelectTokens(keyValuePair.Key).First().ToString(), Is.EqualTo(keyValuePair.Value));
            }
        }
    }
}
