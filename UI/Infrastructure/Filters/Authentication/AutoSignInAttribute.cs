using System;
using System.Web;
using System.Web.Mvc.Filters;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Infrastructure.Mvc;

namespace ZigmaWeb.UI.Infrastructure.Filters.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AutoSignInAttribute : Attribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            int userId;
            var user = filterContext.HttpContext.User;
            if (!user.Identity.IsAuthenticated) return;

            var userIdentity = SessionManager.GetUserIdentity();
            if (userIdentity != null) {
                HttpContext.Current.User = userIdentity;
                return;
            }
            if (!Int32.TryParse(user.Identity.Name, out userId)) {
                return;
            }
            var userManager = new UserService();
            if (userManager.SignInByUserId(userId, out userIdentity)) {
                SessionManager.StoreUserIdentity(userIdentity);
                HttpContext.Current.User = userIdentity;
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }
    }
}