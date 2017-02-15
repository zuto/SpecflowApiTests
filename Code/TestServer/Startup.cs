using System.Web.Http;
using Microsoft.Owin;
using Owin;
using TestServerTest;

[assembly: OwinStartup(typeof(Startup))]
namespace TestServerTest
{    
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);
        }        
    }
}