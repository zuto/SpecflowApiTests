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

        [BeforeScenario("ApiTests")]
        public void BeforeApiTests()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();
        }



        [Given(@"I make a (.*) request with url parameters")]
        public void GivenIMakeAGETRequestWithUrlParameters(string verb, Table table)
        {
            var method = (HttpMethod)Enum.Parse(typeof(HttpMethod), verb);
            HttpRequestMessage.Method = method;
            var @params = "?";
            foreach (var keyValuePair in table.Rows[0])
            {
                @params += keyValuePair.Key + "=" + keyValuePair.Value +"&";
            }
            @params = @params.Remove(@params.Length - 1);
            UrlParameters = @params;
        }

        [When(@"I call the api")]
        public void WhenICallTheApi()
        {
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }
        [Then(@"the api should return a response")]
        public void ThenTheApiShouldReturnAResponse()
        {
            ScenarioContext.Current.Pending();
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
