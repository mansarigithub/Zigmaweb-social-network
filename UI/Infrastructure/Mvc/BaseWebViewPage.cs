using System.Web.Mvc;
using ZigmaWeb.Security.Identity;

namespace ZigmaWeb.UI
{
    public abstract class BaseWebViewPage : WebViewPage
    {
        public virtual new UserIdentity User
        {
            get { return base.User as UserIdentity; }
        }
    }

    public abstract class BaseWebViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual new UserIdentity User
        {
            get { return base.User as UserIdentity; }
        }
    }
}