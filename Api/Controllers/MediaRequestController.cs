using Business;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/request")]
    public class MediaRequestController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(MediaRequestContext.GetMultipleByUserName(ClaimsPrincipal.Current.Identity.GetUserId()));
        }

        [Route("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]MediaRequest request)
        {
            MediaRequestContext.Save(request);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Dependency]
        public IMediaRequestContext MediaRequestContext { get; set; }
    }
}
