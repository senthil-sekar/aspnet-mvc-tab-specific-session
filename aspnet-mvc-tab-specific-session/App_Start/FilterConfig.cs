using System.Web;
using System.Web.Mvc;

namespace aspnet_mvc_tab_specific_session
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
