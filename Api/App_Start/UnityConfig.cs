using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using System.Web.Http.Filters;
using Api.Filters;
using Api.Utils;
using Business;
using Unity.WebApi;
using IExceptionFilter = System.Web.Http.Filters.IExceptionFilter;
using IFilterProvider = System.Web.Http.Filters.IFilterProvider;

namespace Api.App_Start
{
    public static class UnityConfig
    {
        public static void Register(HttpConfiguration config)
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers
            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<IDatabase, Database>();
            container.RegisterType<IErrorManager, ErrorManager>();
            container.RegisterType<IMediaRequestContext, MediaRequestContext>();
            container.RegisterType<IUserContext, UserContext>();
            container.RegisterType<ICache, Cache>();
            container.RegisterType<IConfiguration, Configuration>();
            container.RegisterType<IRouteManager, RouteManager>();

            config.DependencyResolver = new UnityDependencyResolver(container);

            //Register global filters here
            config.Filters.Add((IExceptionFilter)config.DependencyResolver.GetService(typeof(ExceptionHandlerAttribute)));
            //Register the filter injector
            config.Services.Add(typeof(IFilterProvider), new UnityFilterProvider(container));
            var providers = config.Services.GetFilterProviders().ToList();
            var defaultprovider = providers.First(p => p is ActionDescriptorFilterProvider);
            config.Services.Remove(typeof(IFilterProvider), defaultprovider);
        }
    }
}