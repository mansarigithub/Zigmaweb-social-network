using System.Web.Security;
using ZigmaWeb.Security.Identity;

namespace ZigmaWeb.UI.Infrastructure.Mvc
{
    public static class CookieManager
    {
        public static void WriteAuthenticationCookie(UserIdentity user, bool rememberMe)
        {
            FormsAuthentication.SetAuthCookie(user.UserId.ToString(), rememberMe);
        }

        public static void RemoveAuthenticationCookie()
        {
            FormsAuthentication.SignOut();
        }
    }
}