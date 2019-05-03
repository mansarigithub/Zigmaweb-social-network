using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class EducationalResumeBiz : BizBase<EducationalResume>
    {
        public EducationalResumeBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<EducationalResume> ReadUserEducationalResumes(int userId)
        {
            return Read(x => x.UserId == userId)
                .OrderByDescending(x => x.EducationGrade);
        }
    }
}