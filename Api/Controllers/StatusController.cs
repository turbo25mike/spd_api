using System;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace Api.Controllers
{
    [RoutePrefix("api/status")]
    public class StatusController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse("Looking Good!");
        }

        [Route("environment")]
        public HttpResponseMessage GetEnvironment()
        { 
            return Request.CreateResponse(Environment.GetEnvironmentVariable("APP_ENVIRONMENT") ?? "Development");
        }

        [Authorize]
        [Route("secure")]
        public HttpResponseMessage GetSecured()
        {
            var userId = ClaimsPrincipal.Current.Identity.GetUserId();
            return Request.CreateResponse($"Hello, {userId}! You are currently authenticated.");
        }
    }
}
