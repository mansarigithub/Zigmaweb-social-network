using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;

namespace ZigmaWeb.Bussines.Core
{
    public class ExceptionLogBiz : BizBase<ExceptionLog>
    {
        public ExceptionLogBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public void AddException(Exception exception, string url)
        {
            Add(new ExceptionLog() {
                Type = exception.GetType().Name,
                Message = exception.Message,
                InnerMessage = exception.InnerException?.Message,
                HttpRequestUrl = url
            });
        }
    }
}