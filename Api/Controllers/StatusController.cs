using System;
using System.Net.Http;
using System.Web.Http;

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

        [Route("exception")]
        public HttpResponseMessage GetException()
        {
            throw new Exception();
        }
    }
}