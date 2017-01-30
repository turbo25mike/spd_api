using System;
using System.IdentityModel.Tokens;
using Auth0.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;

namespace Api
{
    public static class Auth0Config
    {
        public static void Register(IAppBuilder appBuilder)
        {
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
        }
    }
}
