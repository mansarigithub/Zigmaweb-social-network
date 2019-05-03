using System.Web.Mvc;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Areas.User.ViewModels.Resume;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class ResumeController : BaseController
    {
        public UserResumeService UserResumeService { get; set; }
        public UserProfileService UserProfileService { get; set; }
        public BasicInfoService BasicInfoService { get; set; }

        public ResumeController()
        {
            UserResumeService = new UserResumeService();
            UserProfileService = new UserProfileService();
            BasicInfoService = new BasicInfoService();
       }

        public ActionResult Index()
        {
            var urlsAndStats = UserProfileService.ReadProfileStatisticsAndSocialLinks(User.UserId);
            var viewModel = new ResumeViewModel() {
                ProfileStatisticsAndSocialLinks = urlsAndStats,
            };
            return View(viewModel);
        }

        #region Educational Resume
        [HttpGet]
        public ActionResult ReadEducationalResume(DataSourceRequest request)
        {
            return Json(UserResumeService.ReadUserEducationalResumes(User.UserId, request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEducationalResume(ResumeViewModel viewModel)
        {
            UserResumeService.AddEducationalResume(User, viewModel.EducationalResume);
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteEducationalResume(int id)
        {
            UserResumeService.DeleteEducationalResume(User, id);
            return Json("");
        }

        [HttpGet]
        public ActionResult SuggestOrganizationNameForEducationalResume(string id)
        {
            return Json(new {
                items = BasicInfoService.SuggestOrganization(id, OrganizationType.University),
                phrase = id
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SuggestUniversityFieldName(string id)
        {
            return Json(new {
                items = BasicInfoService.SuggestUniversityFieldName(id),
                phrase = id
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Educational Resume
        [HttpGet]
        public ActionResult ReadJobResume(DataSourceRequest request)
        {
            return Json(UserResumeService.ReadUserJobResumes(User.UserId, request), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddJobResume(ResumeViewModel viewModel)
        {
            UserResumeService.AddJobResume(User, viewModel.JobResume);
            return Json("");
        }

        [HttpPost]
        public ActionResult DeleteJobResume(int id)
        {
            UserResumeService.DeleteJobResume(User, id);
            return Json("");
        }

        [HttpGet]
        public ActionResult SuggestOrganizationNameForJobResume(string id)
        {
            return Json(new {
                items = BasicInfoService.SuggestOrganization(id, OrganizationType.Company),
                phrase = id
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult SuggestJobName(string id)
        {
            return Json(new {
                items = BasicInfoService.SuggestJob(id),
                phrase = id
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}