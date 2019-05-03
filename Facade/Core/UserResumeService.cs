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
using ZigmaWeb.PresentationModel.Model.JobResume;

namespace ZigmaWeb.Facade.Core
{
    public class UserResumeService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        OrganizationBiz OrganizationBiz { get; set; }
        UniversityFieldBiz UniversityFieldBiz { get; set; }
        EducationalResumeBiz EducationalResumeBiz { get; set; }
        JobResumeBiz JobResumeBiz { get; set; }
        JobBiz JobBiz { get; set; }

        public UserResumeService()
        {
            UnitOfWork = new CoreUnitOfWork();
            OrganizationBiz = new OrganizationBiz(UnitOfWork);
            UniversityFieldBiz = new UniversityFieldBiz(UnitOfWork);
            EducationalResumeBiz = new EducationalResumeBiz(UnitOfWork);
            JobResumeBiz = new JobResumeBiz(UnitOfWork);
            JobBiz = new JobBiz(UnitOfWork);
        }

        #region Educational Resume
        public DataSourceResult ReadUserEducationalResumes(int userId, DataSourceRequest request)
        {
            return EducationalResumeBiz.Read(r => r.UserId == userId)
                .OrderByDescending(resume => resume.EducationGrade)
                .MapTo<EducationalResumePM>()
                .ToDataSourceResult(request);
        }

        public void AddEducationalResume(UserIdentity userIdentity, EducationalResumePM educationalResumePM)
        {
            var resume = educationalResumePM.GetEducationalResume();
            if ((resume.StartYear.HasValue && resume.EndYear.HasValue && resume.StartYear > resume.EndYear) ||
                (!resume.StartYear.HasValue || resume.EndYear.HasValue))
                resume.StartYear = resume.EndYear = null;
            resume.UserId = userIdentity.UserId;
            resume.Organization = OrganizationBiz.AddOrganizationIfNotExist(educationalResumePM.OrganizationName, OrganizationType.University);
            resume.UniversityField = UniversityFieldBiz.AddIfNotExist(educationalResumePM.UniversityFieldName);
            EducationalResumeBiz.Add(resume);
            UnitOfWork.SaveChanges();
        }

        public void DeleteEducationalResume(UserIdentity user, int id)
        {
            var resume = EducationalResumeBiz.Find(id);
            if (resume.UserId != user.UserId)
                throw new BusinessException();
            EducationalResumeBiz.Remove(resume);
            UnitOfWork.SaveChanges();
        }
        #endregion

        #region Job Resume
        public DataSourceResult ReadUserJobResumes(int userId, DataSourceRequest request)
        {
            return JobResumeBiz.Read(r => r.UserId == userId)
                .OrderByDescending(resume => resume.StartYear)
                .MapTo<JobResumePM>()
                .ToDataSourceResult(request);
        }

        public void AddJobResume(UserIdentity userIdentity, JobResumePM jobResumePM)
        {
            var resume = jobResumePM.GetJobResume();
            if ((resume.StartYear.HasValue && resume.EndYear.HasValue && resume.StartYear > resume.EndYear) ||
                (!resume.StartYear.HasValue || resume.EndYear.HasValue))
                resume.StartYear = resume.EndYear = null;
            resume.UserId = userIdentity.UserId;
            resume.Organization = OrganizationBiz.AddOrganizationIfNotExist(jobResumePM.OrganizationName, OrganizationType.Company);
            resume.Job = JobBiz.AddJobIfNotExist(jobResumePM.JobName);
            JobResumeBiz.Add(resume);
            UnitOfWork.SaveChanges();
        }

        public void DeleteJobResume(UserIdentity user, int id)
        {
            var resume = JobResumeBiz.Find(id);
            if (resume.UserId != user.UserId)
                throw new BusinessException();
            JobResumeBiz.Remove(resume);
            UnitOfWork.SaveChanges();
        }
        #endregion

    }
}
