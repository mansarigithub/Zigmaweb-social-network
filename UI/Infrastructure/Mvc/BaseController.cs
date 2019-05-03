using Common;
using Common.Exception;
using Common.Web.Mvc;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.Security.Identity;

namespace ZigmaWeb.UI.Controllers
{
    public class BaseController : Controller
    {
        protected virtual new UserIdentity User
        {
            get { return HttpContext.User as UserIdentity; }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            // Log Error
            var logService = new LogService();
            var url = filterContext?.RequestContext?.HttpContext?.Request?.Url?.ToString();
            logService.LogException(filterContext.Exception, url);

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            if (filterContext.Exception is BusinessException || filterContext.HttpContext.Request.IsAjaxRequest()) {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.ClearContent();
                filterContext.Result = Json(new ProcessResult() {
                    Type = ProcessResultType.Error,
                    Message = filterContext.Exception.Message,
                });
                return;
            }

#if DEBUG
#else
            // show error page
            filterContext.HttpContext.Response.Clear();
            filterContext.Result = View("_Error");
            filterContext.ExceptionHandled = true;
#endif
            //base.OnException(filterContext);
        }
    }
}