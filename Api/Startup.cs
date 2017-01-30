using System.Web.Http;
using Microsoft.Owin.Cors;
using Owin;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll); //must be first
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            UnityConfig.Register(httpConfiguration);
            Auth0Config.Register(appBuilder);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}