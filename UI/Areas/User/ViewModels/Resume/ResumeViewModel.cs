using System;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.JobResume;
using ZigmaWeb.PresentationModel.Model.Organization;

namespace ZigmaWeb.UI.Areas.User.ViewModels.Resume
{
    public class ResumeViewModel
    {
        public ProfileStatisticsAndSocialLinksPM ProfileStatisticsAndSocialLinks { get; set; }
        public EducationalResumePM EducationalResume { get; set; }
        public JobResumePM JobResume { get; set; }

    }
}