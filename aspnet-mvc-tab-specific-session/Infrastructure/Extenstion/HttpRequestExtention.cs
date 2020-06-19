using System;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session.Extention
{
    /// <summary>
    /// Extension class for HttpRequest object
    /// </summary>
    public static class HttpRequestExtention
    {
        /// <summary>
        /// Get the absolute uri before route which includes virtual directory.
        /// E.g. For given URL like https://www.example.com/virtual-directory-path/main/index, returns https://www.example.com/virtual-directory-path
        /// </summary>
        /// <param name="requestContext">HTTP request that matches a defined route.</param>
        /// <returns>Absolute Uri before route</returns>
        public static string AbsoluteUriBeforeRoute(this RequestContext requestContext)
        {
            string controller = Convert.ToString(requestContext.RouteData.Values["controller"]);
            Uri uriAddress = requestContext.HttpContext.Request.Url;  // May need to parse out request headers to determine scheme and environment

            //For given URL like https://my.jewelersmutual.com/jewelry-insurance-quote-apply/main/index

            //This code will give us https://my.jewelersmutual.com
            string result = uriAddress.GetLeftPart(UriPartial.Authority);

            //This code will give us https://my.jewelersmutual.com/jewelry-insurance-quote-apply/
            foreach (var seg in uriAddress.Segments)
            {
                //Break when controller starts
                if (seg.TrimEnd('/').Equals(controller, StringComparison.OrdinalIgnoreCase))
                    break;

                result += seg;
            }

            //Finally remove the trailing "/"
            if (result.EndsWith("/"))
            {
                result = result.TrimEnd('/');
            }

            return result;
        }
    }
}