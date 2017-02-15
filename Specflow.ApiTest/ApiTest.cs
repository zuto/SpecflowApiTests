using NUnit.Framework;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;

namespace Specflow.ApiTests
{    
    [Binding]
    public class SpecFlowApiTests
    {
        public static HttpClient HttpClient { get; private set; }
        public static HttpRequestMessage HttpRequestMessage { get; private set; }
        public static HttpResponseMessage HttpResponseMessage { get; private set; }
        private static string _resource;
        private static bool _verbose = false;

        public static void SwapOutHttpClient(HttpClient client)
        {
            HttpClient = client;
        }

        [BeforeScenario]
        public void Before()
        {
            if (HttpClient == null)
                HttpClient = new HttpClient();

            if (HttpRequestMessage == null)
                HttpRequestMessage = new HttpRequestMessage();

            if (_verbose)
                Console.WriteLine($"Using HttpClient with base url: {HttpClient.BaseAddress}");
        }

        [BeforeScenario("Verbose")]
        public void BeforeVerbose()
        {
            _verbose = true;
        }

        [Given(@"I am using the base url `(.*)`")]
        public void GivenIAmUsingTheBaseUrl(string baseUrl)
        {
            if(HttpClient.BaseAddress == null) HttpClient.BaseAddress = new Uri(baseUrl);
        }
        [Given(@"I am using the base url from config setting `(.*)`")]
        public void GivenIAmUsingTheBaseUrlFromConfigSetting(string configKey)
        {            
            var baseUrl = ConfigurationManager.AppSettings[configKey];
            GivenIAmUsingTheBaseUrl(baseUrl);
        }        

        [Given(@"I set default header `(.*)` with value `(.*)`")]
        public void GivenISupplyADefaultRequestHeader(string headerName, string headerValue)
        {
            HttpClient.DefaultRequestHeaders.Add(headerName, headerValue);
        }

        [Given(@"I setup the request to (.*) for resource `(.*)`")]
        public void GivenIMakeARequestForResource(string verb, string path)
        {
            HttpRequestMessage = new HttpRequestMessage();
            HttpRequestMessage.Method = new HttpMethod(verb);
            _resource = path;
        }





        [Given(@"I set header `(.*)` with value `(.*)`")]
        public void GivenISupplyARequestHeaderWithValue(string headerName, string headerValue)
        {
            HttpRequestMessage.Headers.Add(headerName, headerValue);
        }

        [Given(@"I set the request content with Json")]
        public void GivenISetTheRequestContentTypeWithJson(string content)
        {
            GivenISetTheRequestContentAs("application/json", content);
        }

        [Given(@"I set the request content as `(.*)`")]
        public void GivenISetTheRequestContentAs(string contentType, string content)
        {
            HttpRequestMessage.Content = new StringContent(content, Encoding.UTF8, contentType);
        }

        //[Given(@"I set the request content type with MultipartContent and files")]
        //public void GivenISetTheRequestContentTypeWithMultipart(Table files)
        //{
        //    HttpRequestMessage.Content = new MultipartContent();
        //    foreach (var file in files.Rows[0])
        //    {
        //        var path = file.Value;
        //        var content = new ByteArrayContent(File.ReadAllBytes(path));
        //        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("File") { FileName = path };
        //        ((MultipartContent)HttpRequestMessage.Content).Add(content);
        //    }
        //}

        [When(@"I send the request")]
        public void WhenISendTheRequest()
        {
            HttpRequestMessage.RequestUri = new Uri(_resource, UriKind.Relative);
            HttpResponseMessage = HttpClient.SendAsync(HttpRequestMessage).Result;
        }



        [Then(@"I should receive a response")]
        public void ThenIShouldReceiveAResponse()
        {
            if (_verbose)
            {
                try
                {
                    Console.WriteLine(HttpRequestMessage);
                    Console.WriteLine(HttpRequestMessage.Content.ReadAsStringAsync().Result);
                }
                catch
                {
                }
                try
                {
                    Console.WriteLine(HttpResponseMessage);
                    Console.WriteLine(HttpResponseMessage.Content.ReadAsStringAsync().Result);
                }
                catch
                {
                }
            }
        }

        [Then(@"I should have a status code of (.*)")]
        public void ThenTheStatusCodeIs(int statusCode)
        {
            Assert.That((int)HttpResponseMessage.StatusCode, Is.EqualTo(statusCode));
        }
        [Then(@"I should have status code that is a success code")]
        public void ThenTheStatusCodeIsASuccessStatusCode()
        {
            Assert.IsTrue(HttpResponseMessage.IsSuccessStatusCode, "Expected successful status code");
        }
        [Then(@"I should have status code that is not a success code")]
        public void ThenTheStatusCodeIsNotASuccessStatusCode()
        {
            Assert.IsFalse(HttpResponseMessage.IsSuccessStatusCode, "Expected non-successful status code");
        }

        [Then(@"I should have a content type of `(.*)`")]
        public void ThenTheApiResponseShouldHaveAContentTypeOf(string contentType)
        {
            Assert.That(HttpResponseMessage.Content.Headers.ContentType.MediaType, Is.EqualTo(contentType));
        }

        [Then(@"I should have content matching")]
        public void ThenTheApiResponseShouldHaveContentMatching(string match)
        {
            Assert.That(HttpResponseMessage.Content.ReadAsStringAsync().Result, Is.EqualTo(match));
        }

        [Then(@"I should have content partially matching")]
        public void ThenTheApiResponseShouldHaveContentPartiallyMatching(string match)
        {
            Assert.That(HttpResponseMessage.Content.ReadAsStringAsync().Result, Is.StringContaining(match));
        }

        [Then(@"I should have content matching `(.*)`")]
        public void ThenTheApiResponseShouldHaveContentMatchingValue(string value)
        {
            ThenTheApiResponseShouldHaveContentMatching(value);
        }

        [Then(@"I should have content partially matching `(.*)`")]
        public void ThenTheApiResponseShouldHaveContentPartiallyMatchingValue(string value)
        {
            ThenTheApiResponseShouldHaveContentPartiallyMatching(value);
        }

        [Then(@"I should have header `(.*)` with value `(.*)`")]
        public void ThenIShouldHaveHeaderWithValue(string key, string value)
        {
            Assert.That(HttpResponseMessage.Headers.GetValues(key).FirstOrDefault(), Is.EqualTo(value));
        }

        [Then(@"I should have content header `(.*)` with value `(.*)`")]
        public void ThenIShouldHaveContentHeaderWithValue(string key, string value)
        {
            Assert.That(HttpResponseMessage.Content.Headers.GetValues(key).FirstOrDefault(), Is.EqualTo(value));
        }

    }
}
