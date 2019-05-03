using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Infrastructure.SignalR;
using ZigmaWeb.UI.ViewModels.Article;
using ZigmaWeb.UI.ViewModels.Search;
using ZigmaWeb.UI.ViewModels.Tag;

namespace ZigmaWeb.UI.Controllers
{
    public class SearchController : BaseController
    {
        public ArticleService ArticleService { get; set; }

        public SearchController()
        {
            ArticleService = new ArticleService();
        }

        [Route("search/", Name = "Search")]
        public ActionResult Index(string query)
        {
            ViewBag.SearchKeyword = query;
            var viewModel = new SearchViewModel()
            {
                Articles = ArticleService.SearchArticles(query),
                Keyword = query,
            };
            return View(viewModel);
        }
    }
}