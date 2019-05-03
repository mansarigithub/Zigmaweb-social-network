using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ZigmaWeb.Validation.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
    public class ValidateModelAttribute : FilterAttribute, IActionFilter
    {
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest, "BadRequest");
            }
        }

        void IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}
