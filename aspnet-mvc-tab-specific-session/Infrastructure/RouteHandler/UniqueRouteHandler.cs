using aspnet_mvc_tab_specific_session.Extention;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session.RouteHandler
{
    /// <summary>
    /// Handle requests with no tab id in the URL
    /// </summary>
    public class UniqueRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            //Dont redirect for Post action, as we may loose the form data
            if (requestContext.HttpContext.Request.HttpMethod == HttpMethod.Post.ToString())
            {
                return base.GetHttpHandler(requestContext);
            }

            //Get current unique id from RouteData
            var guidString = Convert.ToString(requestContext.RouteData.Values["tabid"]);

            //if true, then construct new url with tab id
            if (!string.IsNullOrEmpty(guidString))
            {
                //create new guid
                guidString = ShortGuid.NewGuid().ToString();

                //construct redirect url
                string rootUrl = requestContext.AbsoluteUriBeforeRoute();
                string controller = Convert.ToString(requestContext.RouteData.Values["controller"]);
                string action = Convert.ToString(requestContext.RouteData.Values["action"]);
                string id = Convert.ToString(requestContext.RouteData.Values["id"]);
                string queryString = requestContext.HttpContext.Request.Url.Query;

                string url = $"{rootUrl}/t/{guidString}";

                bool skipToQueryString = false;

                if (controller.Equals("Main", StringComparison.OrdinalIgnoreCase) &&
                    action.Equals("Index", StringComparison.OrdinalIgnoreCase))
                {
                    skipToQueryString = true;
                }

                if(!skipToQueryString)
                {
                    url += $"/{controller}";

                    if (!action.Equals("Index", StringComparison.OrdinalIgnoreCase))
                    {
                        url += $"/{action}";
                    }
                }

                if (!string.IsNullOrWhiteSpace(id))
                {
                    url += $"/{id}";
                }

                url += queryString;

                //redirect permanantly to unique tab url
                requestContext.HttpContext.Response.Redirect(url, true);
            }

            return base.GetHttpHandler(requestContext);
        }
    }
}