using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session
{
    public interface IRouteProvider
    {
        void RegisterRoutes(RouteCollection routes, string routeType);
    }
}
