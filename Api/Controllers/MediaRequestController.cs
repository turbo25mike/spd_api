using Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Filters;
using Api.Utils;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [Auth]
    [RoutePrefix("api/request")]
    public class MediaRequestController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            var currentUser = Route.GetData("user") as User;
            return currentUser == null ? Request.CreateErrorResponse(HttpStatusCode.BadRequest, Constants.UserNotFoundError) : Request.CreateResponse(MediaRequestContext.GetMultipleByUserName(currentUser.Name));
        }

        [Route("{id}")]
        public HttpResponseMessage Put(int id, [FromBody]MediaRequest request)
        {
            MediaRequestContext.Save(request);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Dependency]
        public IMediaRequestContext MediaRequestContext { get; set; }
        [Dependency]
        public IRouteManager Route { get; set; }
    }
}
