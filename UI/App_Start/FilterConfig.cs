using System.Web.Mvc;
using ZigmaWeb.UI.Infrastructure.Filters.Authentication;

namespace ZigmaWeb.UI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AutoSignInAttribute());
        }
    }
}
