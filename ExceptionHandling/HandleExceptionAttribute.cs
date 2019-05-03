//using Common.Exception;
//using System;
//using System.Web.Mvc;

//namespace Common.Web.Mvc.Attributes
//{
//    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false)]
//    public class HandleException : FilterAttribute, IExceptionFilter
//    {
//        public void OnException(ExceptionContext filterContext)
//        {
//            ProcessResult result = new ProcessResult();
//            result.ResultType = (filterContext.Exception is BusinessException ? ProcessResultType.Warning : ProcessResultType.Error);
//            result.Message = (result.ResultType == ProcessResultType.Warning ? filterContext.Exception.Message : "");
            
//            JsonResult jsonResult = new JsonResult();
//            jsonResult.Data = result;
//            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
//            filterContext.Result = jsonResult;
//            filterContext.ExceptionHandled = true;
//        }
//    }

//}

