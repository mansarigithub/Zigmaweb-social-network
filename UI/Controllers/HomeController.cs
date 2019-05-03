using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.ViewModels.Home;

namespace ZigmaWeb.UI.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        public AppStatisticsService AppStatistics { get; set; }

        public HomeController()
        {
            AppStatistics = new AppStatisticsService();
        }

        public ActionResult Index()
        {
            var viewModel = new HomeViewModel(AppStatistics.ReadStatisticsForHomePage());
            return View(viewModel);
        }

        [Route("about")]
        public ActionResult AboutUs()
        {
            return View();
        }

        [Route("privacy")]
        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        [Route("terms", Order = 1)]
        public ActionResult TermsOfUse()
        {
            return View();
        }

        [Route("faq", Order = 1)]
        public ActionResult FAQ()
        {
            return View();
        }

        [Route("article")]
        public ActionResult SendArticle()
        {
            return View();
        }

        [Route("makeblog")]
        public ActionResult MakeBlog()
        {
            return View();
        }

        //[Route("search")]
        //public ActionResult Search()
        //{
        //    return View();
        //}

        [Route("introduction")]
        public ActionResult Introduction()
        {
            return View();
        }
        //[Route("Test")]
        //public ActionResult Test()
        //{
        //    return View();
        //}

    }
}