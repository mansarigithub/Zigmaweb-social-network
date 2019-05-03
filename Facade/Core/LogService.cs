using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System;
using System.Collections;
using ZigmaWeb.PresentationModel.Model;
using System.Collections.Generic;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.Facade.Core
{
    public class LogService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        ExceptionLogBiz ExceptionLogBiz { get; set; }

        public LogService()
        {
            UnitOfWork = new CoreUnitOfWork();
            ExceptionLogBiz = new ExceptionLogBiz(UnitOfWork);
        }

        public void LogException(Exception exception, string url)
        {
            ExceptionLogBiz.AddException(exception, url);
            UnitOfWork.SaveChanges();
        }
    }
}
