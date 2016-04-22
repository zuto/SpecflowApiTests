using Microsoft.Owin.Testing;
using TechTalk.SpecFlow;
using TestServerTest;

namespace Specflow.ApiTest.Tests
{
    [Binding]
    public static class MySteps
    {
        public static TestServer Server { get; set; }

        [Given(@"I am running the test server")]
        public static void GivenIAmRunningTheTestServer()
        {
            Server = TestServer.Create<Startup>();
            ApiTests.SpecFlowApiTests.SwapOutHttpClient(Server.HttpClient);
        }
    }
}