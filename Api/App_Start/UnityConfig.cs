using System.Linq;
using System.Web.Http;
using System.Web.Http.Filters;
using Api.Filters;
using Business;
using Microsoft.Practices.Unity;
using Unity.WebApi;

namespace Api
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

            config.DependencyResolver = new UnityDependencyResolver(container);

            //Register global filters here
            config.Filters.Add((IExceptionFilter)config.DependencyResolver.GetService(typeof(ExceptionHandlerAttribute)));
            //Register the filter injector
            config.Services.Add(typeof(IFilterProvider), new UnityFilterProvider(container));
            var providers = config.Services.GetFilterProviders().ToList();
            var defaultprovider = providers.FirstOrDefault(p => p is ActionDescriptorFilterProvider);
            if(defaultprovider != null)
                config.Services.Remove(typeof(IFilterProvider), defaultprovider);
        }
    }
}