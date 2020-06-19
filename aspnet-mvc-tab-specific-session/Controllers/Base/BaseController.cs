using Newtonsoft.Json;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace aspnet_mvc_tab_specific_session.Controllers
{
    public abstract class BaseController : Controller
    {
        protected internal string UniqueKey
        {
            get
            {
                return Convert.ToString(this.RouteData.Values["tabid"]) ?? string.Empty;
            }
        }

        public new UniqueSession Session
        {
            get
            {
                return new UniqueSession(UniqueKey);
            }
        }

        public new UniqueTempData TempData
        {
            get
            {
                return new UniqueTempData(UniqueKey, base.TempData);
            }
        }

        protected internal new RedirectToRouteResult RedirectToAction(string actionName, string controllerName)
        {
            return string.IsNullOrEmpty(UniqueKey) ? base.RedirectToAction(actionName, controllerName)
               : base.RedirectToAction(actionName, controllerName, new RouteValueDictionary { { "tabid", UniqueKey } });
        }

        protected internal new RedirectToRouteResult RedirectToAction(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            if (!string.IsNullOrEmpty(UniqueKey))
            {
                routeValues.Add("tabid", UniqueKey);
            }
            return base.RedirectToAction(actionName, controllerName, routeValues);
        }

        protected internal ContentResult JsonContent(object content)
        {
            return Content(JsonConvert.SerializeObject(content), "application/json");
        }
    }
}