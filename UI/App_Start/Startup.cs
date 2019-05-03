using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ZigmaWeb.UI.Startup))]
namespace ZigmaWeb.UI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}