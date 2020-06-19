using aspnet_mvc_tab_specific_session.RouteHandler;
using System.Web.Mvc;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Important!!! Order of the route is important. Please do not alter.

            //This route is for URL with tab id
            //Keep this route always at the top
            routes.Add("UniqueRoute", new UniqueRoute(
                "t/{tabid}/{controller}/{action}/{id}",
                new
                {
                    controller = "Home",
                    action = "Index",
                    tabid = "",
                    id = UrlParameter.Optional
                },
                new
                {
                    tabid = new GuidRouteConstraint()
                },
                new MvcRouteHandler()));

            //This route is to support script bundles and will be handled in regular way without tab id
            routes.MapRoute(
                name: "ScriptBundleRoute",
                url: "bundles/{action}",
                defaults: new { controller = "bundles", action = "jquery" });

            //This route is to support stylesheet bundles and will be handled in regular way without tab id
            routes.MapRoute(
                name: "StyleBundleRoute",
                url: "Content/{action}",
                defaults: new { controller = "Content", action = "css" });

            //This route is support all generic urls. UniqueRouteHandler will handle the request and append the tab id
            //Keep this route always at the bottom
            routes.Add("Default", new UniqueRoute(
                "{controller}/{action}/{id}",
                new
                {
                    controller = "Main",
                    action = "Index",
                    tabid = "",
                    id = UrlParameter.Optional
                },
                new UniqueRouteHandler()));
        }
    }
}
