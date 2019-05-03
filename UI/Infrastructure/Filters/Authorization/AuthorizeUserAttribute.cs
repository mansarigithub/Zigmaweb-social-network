using System;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.UI.Infrastructure.Filters.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        string[] _roles;
        string[] _users;

        public AuthorizeUserAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (_roles == null)
                _roles = string.IsNullOrWhiteSpace(Roles) ? new string[0] : Roles.ToLower().Split(',');
            if (_users == null)
                _users = string.IsNullOrWhiteSpace(Users) ? new string[0] : Users.ToLower().Split(',');

            var principal = httpContext.Session[SessionKeys.UserIdentity.ToString()] as UserIdentity;
            if (principal == null) return false;
            HttpContext.Current.User = principal;
            if (_roles.Length > 0 && !_roles.Any(principal.IsInRole))
                return false;
            if (_users.Length > 0 && !_users.Contains(principal.Identity.Name, StringComparer.OrdinalIgnoreCase))
                return false;

            return true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}