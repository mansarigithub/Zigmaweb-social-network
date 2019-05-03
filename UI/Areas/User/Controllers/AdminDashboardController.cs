using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.DataProvider;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Areas.User.ViewModels.AdminDashboard;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser(Roles = "admin")]
    public class AdminDashboardController : BaseController
    {
        private ContentService ContentService { get; }
        private UserService UserService { get; }
        private AppStatisticsService AppStatisticsService { get; }

        public AdminDashboardController()
        {
            ContentService = new ContentService();
            UserService = new UserService();
            AppStatisticsService = new AppStatisticsService();
        }

        public ActionResult Index()
        {
            var container = AppStatisticsService.GetAdminDashboardStatistics();
            var viewModel = new AdminDashboardViewModel(container);
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult GetAllContents(DataSourceRequest request)
        {
            return Json(ContentService.GetAllContents(request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllUsers(DataSourceRequest request)
        {
            return Json(UserService.GetAllUsers(request), JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult GetSiteVisitChartData()
        {
            var result = AppStatisticsService.GetSiteTotalVisistsByDate().ToList();
            return GetChartData(result);
        }

        [HttpGet]
        public ActionResult GetUsersRegistrationStatisticChartData()
        {
            var result = AppStatisticsService.GetUsersRegistrationStatisticByDate().ToList();
            return GetChartData(result);
        }

        [HttpGet]
        public ActionResult GetArticlePublishStatisticChartData()
        {
            var result = AppStatisticsService.GetArticlePublishStatisticByDate().ToList();
            return GetChartData(result);
        }

        [HttpGet]
        public ActionResult GetBlogPostPublishStatisticChartData()
        {
            var result = AppStatisticsService.GetBlogPostPublishStatisticByDate().ToList();
            return GetChartData(result);
        }

        private ActionResult GetChartData(List<ChartData> result)
        {
            ChartDataHelper.FillBlankDays(result);
            var chartData = result.Select(cd => new {
                Date = cd.Date.ToPersianDate("MMDD"),
                Visits = cd.Value
            });
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }

    }
}