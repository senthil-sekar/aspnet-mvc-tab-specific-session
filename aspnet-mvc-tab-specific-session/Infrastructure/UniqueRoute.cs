using System.Web;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session
{
    /// <summary>
    /// This class implement Route definition that will put unique value in RouteData for session
    /// </summary>
    public class UniqueRoute : Route
    {
        private readonly bool isUnique;

        #region Constructors

        public UniqueRoute(string url, object defaults, IRouteHandler routeHandler)
           : base(url, new RouteValueDictionary(defaults), routeHandler)
        {
            isUnique = url.Contains("tabid");
            DataTokens = new RouteValueDictionary();
        }

        public UniqueRoute(string url, object defaults, object constraints, IRouteHandler routeHandler)
           : base(url, new RouteValueDictionary(defaults), new RouteValueDictionary(constraints), routeHandler)
        {
            isUnique = url.Contains("tabid");
            DataTokens = new RouteValueDictionary();
        }

        #endregion

        public override RouteData GetRouteData(HttpContextBase httpContext)
        {
            var routeData = base.GetRouteData(httpContext);
            if (routeData == null)
                return null;

            //Defensive!!! 
            //when true, then it means routing was resolved incorrectly, potentially due to url tampering
            if(routeData.Values["controller"].ToString() == "t")
            {
                return routeData;
            }

            //Inject tab id for very first incoming request or new browser tab
            if (!routeData.Values.ContainsKey("tabid") || routeData.Values["tabid"].ToString() == string.Empty)
                routeData.Values["tabid"] = ShortGuid.NewGuid().ToString();

            return routeData;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
            => !isUnique ? null : base.GetVirtualPath(requestContext, values);
    }
}