using System.Web.Http;
using Api.App_Start;
using Owin;
using System.Configuration;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            //appBuilder.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            UnityConfig.Register(httpConfiguration);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}
