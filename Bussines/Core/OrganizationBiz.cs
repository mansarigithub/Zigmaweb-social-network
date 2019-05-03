using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using ZigmaWeb.Common.Enum;
using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model.Organization;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class OrganizationBiz : BizBase<Organization>
    {
        public OrganizationBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public Organization AddOrganizationIfNotExist(string universityName, OrganizationType orgType)
        {
            universityName = universityName.Trim();
            var university = ReadSingleOrDefault(u => u.Name == universityName);
            if (university != null) return university;
            return Add(new Organization() {
                Type = orgType,
                Name = universityName,
            });
        }

        public IQueryable<Organization> SuggestOrganization(string phrase, OrganizationType orgType)
        {
            return UnitOfWork.OrganizationRepository
                .Read(org => org.Type == orgType &&
                      org.Name.Contains(phrase))
                .Take(5);
        }
    }
}