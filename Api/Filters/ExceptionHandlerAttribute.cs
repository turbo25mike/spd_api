using System;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using Business;
using Microsoft.Practices.Unity;

namespace Api.Filters
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        [Dependency]
        public IErrorManager ErrorManager { get; set; }
       
        public override void OnException(HttpActionExecutedContext context)
        {
            ErrorManager.Handle(context.Exception);
            var statusCode = HttpStatusCode.InternalServerError;
            if (context.Exception is NotImplementedException)
                context.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            context.Response = new HttpResponseMessage(statusCode);
        }
    }
}