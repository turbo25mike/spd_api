using System.Net;
using System.Net.Http;
using System.Web.Http;
using Api.Utils;
using Business;
using Microsoft.Practices.Unity;

namespace Api.Controllers
{
    [RoutePrefix("api/login")]
    public class LoginController : ApiController
    {
        [Route("")]
        public HttpResponseMessage Post(User user)
        {
            if (!UserContext.IsValid(user))
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            var token = Cache.SetItem(user);
            return Request.CreateResponse(HttpStatusCode.OK, token);
        }
        
        [Dependency]
        public IUserContext UserContext { get; set; }
        [Dependency]
        public ICache Cache { get; set; }
    }
}