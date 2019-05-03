using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ZigmaWeb.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ApplicationInitializer.Initialize();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (Request.Url.Host.StartsWith("www") && !Request.Url.IsLoopback)
            {
                UriBuilder builder = new UriBuilder(Request.Url);
                builder.Host = Request.Url.Host.Replace("www.", string.Empty);
                Response.StatusCode = 301;
                Response.AddHeader("Location", builder.ToString());
                Response.End();
            }
        }

        //protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        //{
        //}

        //protected void Application_Error(Object sender, EventArgs e)
        //{
        //    var exception = Server.GetLastError();
        //    if (exception == null)
        //        return;

        //    // Log server error
        //    Common.Loggers.AppLogger.Error("this is a message", exception, string.Empty, string.Empty);
        //}
    }
}