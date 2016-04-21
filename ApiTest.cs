using NUnit.Framework;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace Specflow.ApiTests
{
    [Binding]
    public class SpecFlowApiTests
    {
        public static HttpClient HttpClient { get; set; }
        public static HttpRequestMessage HttpRequestMessage { get; set; }
        public static HttpResponseMessage HttpResponseMessage { get; set; }
        private static string UrlParameters;
        private static string UrlRoute;

        [BeforeScenario("ApiTest")]
        public void BeforeApiTests()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
        }

        [Given(@"I am using the base url (.*)")]
        public void GivenIAmUsingTheBaseUrl(string baseUrl)
        {
            HttpClient.BaseAddress = new Uri(baseUrl);
        }
        [Given(@"I make a (.*) request with url parameters for (.*)")]
        public void GivenIMakeARequestWithUrlParametersForCustomer(string verb, string route, Table table)
        {
            UrlRoute = route;
            GivenIMakeARequestWithUrlParameters(verb, table);
        }
        [Given(@"I supply the header (.*) with value (.*)")]
        public void GivenISupplyTheHeaderAcceptWithValueApplicationJson(string headerName, string headerValue)
        {
            HttpRequestMessage.Headers.Add(headerName, headerValue);
        }

        [Given(@"I make a (.*) request with url parameters")]
        public void GivenIMakeARequestWithUrlParameters(string verb, Table table)
        {
            HttpRequestMessage.Method = new HttpMethod(verb);
            var @params = "?";
            foreach (var keyValuePair in table.Rows[0])
            {
                @params += keyValuePair.Key + "=" + keyValuePair.Value + "&";
            }
            @params = @params.Remove(@params.Length - 1);
            UrlParameters = @params;
        }


        [When(@"I call the api")]
        public void WhenICallTheApi()
        {
            HttpRequestMessage.RequestUri = new Uri(UrlRoute + UrlParameters, UriKind.Relative);
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
    }
}