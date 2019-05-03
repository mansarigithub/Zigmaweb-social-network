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
    [AuthorizeUser]
    public class StatisticsController : BaseController
    {
        private ArticleService ContentService { get; }
        private UserService UserService { get; }
        private AppStatisticsService AppStatisticsService { get; }

        public StatisticsController()
        {
            ContentService = new ArticleService();
            UserService = new UserService();
            AppStatisticsService = new AppStatisticsService();
        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult GetAuthorTotalArticlesVisitsChartData()
        {
            var result = AppStatisticsService.GetAuthorTotalArticlesVisitsChartData(User).ToList();
            return GetChartData(result);
        }

        [HttpGet]
        public ActionResult GetUserProfileVisitsChartData()
        {
            var result = AppStatisticsService.GetAuthorTotalVisitsChartData(User).ToList();
            return GetChartData(result);
        }
        //[HttpGet]
        //public ActionResult GetAuthorTotalBlogPostsChartData()
        //{
        //    var result = AppStatisticsService.GetAuthorTotalBlogPostsChartData(User).ToList();
        //    return GetChartData(result);
        //}

        private ActionResult GetChartData(List<ChartData> result)
        {
            ChartDataHelper.FillBlankDays(result);
            var chartData = result.Select(cd => new { Date = cd.Date.ToNumericPersianDateString(), Visits = cd.Value });
            return Json(chartData, JsonRequestBehavior.AllowGet);
        }
    }
}