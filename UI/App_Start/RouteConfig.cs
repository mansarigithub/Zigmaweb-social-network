using System.Web.Mvc;
using System.Web.Routing;

namespace ZigmaWeb.UI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            AreaRegistration.RegisterAllAreas();

            routes.MapRoute(
                 name: "Publication",
                 url: "{publicationName}",
                 defaults: new { controller = "Publication", action = "Index" },
                 namespaces: new[] { "ZigmaWeb.UI.Controllers" }
             );

            routes.MapRoute(
                 name: "Default",
                 url: "{controller}/{action}/{id}",
                 defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                 namespaces: new[] { "ZigmaWeb.UI.Controllers" }
             );


        }
    }
}
