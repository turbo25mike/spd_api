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

            var domain = Environment.GetEnvironmentVariable("AUTH0_DOMAIN") ?? "";
            var audience = Environment.GetEnvironmentVariable("AUTH0_CLIENT_IDS") ?? "";
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
            appBuilder.UseWebApi(httpConfiguration);
        }
    }
}