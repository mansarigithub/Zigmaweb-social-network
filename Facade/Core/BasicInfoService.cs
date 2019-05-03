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
using ZigmaWeb.PresentationModel.Model.Organization;
using Common.Exception;
using ZigmaWeb.PresentationModel.Model.UniversityField;
using ZigmaWeb.PresentationModel.Model.Job;

namespace ZigmaWeb.Facade.Core
{
    public class BasicInfoService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        OrganizationBiz OrganizationBiz { get; set; }
        UniversityFieldBiz UniversityFieldBiz { get; set; }
        JobBiz JobBiz { get; set; }

        public BasicInfoService()
        {
            UnitOfWork = new CoreUnitOfWork();
            OrganizationBiz = new OrganizationBiz(UnitOfWork);
            UniversityFieldBiz = new UniversityFieldBiz(UnitOfWork);
            JobBiz = new JobBiz(UnitOfWork);
        }

        public IEnumerable<OrganizationPM> SuggestOrganization(string phrase, OrganizationType orgType)
        {
            return OrganizationBiz.SuggestOrganization(phrase, orgType)
                .MapTo<OrganizationPM>()
                .ToList();
        }

        public IEnumerable<UniversityFieldPM> SuggestUniversityFieldName(string phrase)
        {
            return UniversityFieldBiz.SuggestUniversityField(phrase)
                .MapTo<UniversityFieldPM>()
                .ToList();
        }

        public IEnumerable<JobPM> SuggestJob(string phrase)
        {
            return JobBiz.SuggestJob(phrase)
                .MapTo<JobPM>()
                .ToList();
        }
    }
}
