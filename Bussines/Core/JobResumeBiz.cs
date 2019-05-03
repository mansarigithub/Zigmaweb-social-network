using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class JobResumeBiz : BizBase<JobResume>
    {
        public JobResumeBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }
    }
}