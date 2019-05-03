using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZigmaWeb.UI.Infrastructure.Filters
{
    public class RedirectAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //if(filterContext.HttpContext.Request.Url)
        }
    }
}