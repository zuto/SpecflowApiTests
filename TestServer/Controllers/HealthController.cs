using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TestServerTest.Controllers
{
    public class HealthController : ApiController
    {
        [Route("health")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Healthy");
        }
    }
}