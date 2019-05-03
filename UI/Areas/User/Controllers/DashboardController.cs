using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Areas.User.ViewModels.Dashboard;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class DashboardController : BaseController
    {
        private AppStatisticsService AppStatisticsService { get; }

        public DashboardController()
        {
            AppStatisticsService = new AppStatisticsService();
        }

        public ActionResult Index()
        {
            var viewModel = new DashboardViewModel(AppStatisticsService.GetUserDashboardData(User.UserId));
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetArticlesVisitChartData()
        {
            return Json(AppStatisticsService.GetContentVisitChartDataForUserDashboard(User.UserId, ContentType.Article), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetPostsVisitChartData()
        {
            return Json(AppStatisticsService.GetContentVisitChartDataForUserDashboard(User.UserId, ContentType.BlogPost), JsonRequestBehavior.AllowGet);
        }

    }
}