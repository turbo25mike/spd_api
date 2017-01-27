using System;
using System.Configuration;
using System.Linq;
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
            var results = string.Join(",", ConfigurationManager.AppSettings.AllKeys.Select(key => ConfigurationManager.AppSettings[key]));
            return Request.CreateResponse(results);
        }

        [Route("exception")]
        public HttpResponseMessage GetException()
        {
            throw new Exception();
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
