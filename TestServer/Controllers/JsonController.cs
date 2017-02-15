using System.Dynamic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace TestServerTest.Controllers
{
    public class JsonController : ApiController
    {
        [Route("json")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            dynamic value = new ExpandoObject();
            value.Value1 = 1;
            value.ValueHello = "Hello";

            return Request.CreateResponse(HttpStatusCode.OK, (object)value, new JsonMediaTypeFormatter());
        }
    }
}