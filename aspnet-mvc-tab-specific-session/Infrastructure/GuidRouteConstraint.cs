using System;
using System.Web;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session
{
    public class GuidRouteConstraint : IRouteConstraint
    {
        public bool Match
            (
                HttpContextBase httpContext,
                Route route,
                string parameterName,
                RouteValueDictionary values,
                RouteDirection routeDirection
            )
        {
            var inputString = Convert.ToString(values[parameterName]);

            if (!string.IsNullOrEmpty(inputString))
            {
                return ShortGuid.IsValid(inputString);
            }

            return true;
        }
    }
}