using System;
using System.IdentityModel.Tokens;
using System.Web.Http;
using Auth0.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace Api
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCors(CorsOptions.AllowAll); //must be first
            var audience = Environment.GetEnvironmentVariable("AUTH0_CLIENT_IDS") ?? "VRuQOIlWkBs3WEwjwafACwuY43tWZ5Tn";
            var domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN") ?? "https://spd.auth0.com/";
            var keyResolver = new OpenIdConnectSigningKeyResolver(domain);
            appBuilder.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidAudience = audience,
                        ValidIssuer = domain,
                        IssuerSigningKeyResolver = (token, securityToken, identifier, parameters) => keyResolver.GetSigningKey(identifier)
                    }
                });
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MapHttpAttributeRoutes();
            UnityConfig.Register(httpConfiguration);
            //Auth0Config.Register(appBuilder);
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}