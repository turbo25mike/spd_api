using Business;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Filters;
using Api.Utils;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [RoutePrefix("api/sql")]
    public class SqlTestController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(MediaRequestContext.GetAll());
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
