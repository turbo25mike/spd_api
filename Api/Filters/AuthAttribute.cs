using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Api.Utils;
using Business;
using Microsoft.Practices.Unity;

namespace Api.Filters
{
    public class AuthAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var query = actionContext.Request.RequestUri.ParseQueryString();
            if (!string.IsNullOrEmpty(query.Get("token")))
            {
                var token = query.Get("token");
                var currentUser = Cache.GetItem(token);

                if (currentUser != null)
                {
                    Route.SetData("user", currentUser);
                    return;
                }
            }

            actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
        }
        [Dependency]
        public ICache Cache { get; set; }
        [Dependency]
        public IRouteManager Route { get; set; }
    }
}