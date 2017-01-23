using System.Web;

namespace Api.Utils
{
    public interface IRouteManager {
        object GetData(string propName);
        void SetData(string propName, object obj);
    }

    public class RouteManager : IRouteManager
    {
        public object GetData(string propName)
        {
            return HttpContext.Current.Request.RequestContext.RouteData.Values[propName];
        }

        public void SetData(string propName, object obj)
        {
            HttpContext.Current.Request.RequestContext.RouteData.Values.Add(propName, obj);
        }
    }
}