using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class UniversityFieldBiz : BizBase<UniversityField>
    {
        public UniversityFieldBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public UniversityField AddIfNotExist(string universityFieldName)
        {
            universityFieldName = universityFieldName.Trim();
            var universityField = ReadSingleOrDefault(u => u.Name == universityFieldName);
            if (universityField != null) return universityField;
            return Add(new UniversityField() {
                Name = universityFieldName,
            });
        }

        public IQueryable<UniversityField> SuggestUniversityField(string phrase)
        {
            return UnitOfWork.UniversityFieldRepository
                .Read(x => x.Name.Contains(phrase))
                .Take(5);
        }
    }
}