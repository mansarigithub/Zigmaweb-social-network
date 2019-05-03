using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Infrastructure.SignalR;
using ZigmaWeb.UI.ViewModels.Article;
using ZigmaWeb.UI.ViewModels.Tag;

namespace ZigmaWeb.UI.Controllers
{
    public class DirectoryController : BaseController
    {
        public TagService TagService { get; set; }
        public BlogService BlogService { get; set; }
        public ArticleService ArticleService { get; set; }
        public AppStatisticsService AppStatisticsService { get; set; }

        public DirectoryController()
        {
            TagService = new TagService();
            BlogService = new BlogService(SignalRPushNotificationProvider.Instance);
            ArticleService = new ArticleService();
            AppStatisticsService = new AppStatisticsService();
       }

        [Route("directory/tags/page/{pageIndex:int?}", Name = "TagList")]
        public ActionResult Tags(int? pageIndex)
        {
            const int pageSize = 5;
            if (!pageIndex.HasValue) pageIndex = 1;
            var viewModel = new TagListViewModel() {
                 PageIndex = pageIndex.Value,
                 PageSize = pageSize,
                 Tags = TagService.ReadTags(pageIndex.Value, pageSize),
                 TotallRows = TagService.ReadTotallTagsCount()
            };
            return View(viewModel);
        }

        [Route("directory/tags/{tagId}/{pageIndex:int?}", Name = "TagContents")]
        public ActionResult TagContents(int tagId, int? pageIndex)
        {
            const int pageSize = 5;
            if (!pageIndex.HasValue) pageIndex = 1;
            var viewModel = new TagContentsViewModel() {
                Tag = TagService.ReadTag(tagId),
                PageIndex = pageIndex.Value,
                PageSize = pageSize,
                Articles = TagService.ReadTagContents(tagId, pageIndex.Value, pageSize),
                TotalRows = TagService.ReadTagArticlesCount(tagId)
            };
            return View(viewModel);
        }

        [Route("directory/blogs/{pageIndex:int?}", Name = "BlogList")]
        public ActionResult Blogs(int? pageIndex)
        {
            const int pageSize = 5;
            if (!pageIndex.HasValue) pageIndex = 1;
            var viewModel = new BlogListViewModel() {
                PageIndex = pageIndex.Value,
                PageSize = pageSize,
                Blogs = BlogService.ReadBlogs(pageIndex.Value, pageSize),
                TotallRows = BlogService.ReadTotallBlogsCount()
            };
            return View(viewModel);
        }

        [Route("directory/articles/{pageIndex:int?}", Name = "ArticleList")]
        public ActionResult Articles(int? pageIndex)
        {
            const int pageSize = 5;
            if (!pageIndex.HasValue) pageIndex = 1;
            var viewModel = new ArticleListViewModel() {
                PageIndex = pageIndex.Value,
                PageSize = pageSize,
                Articles = ArticleService.ReadArticles(pageIndex.Value, pageSize),
                TotallRows = ArticleService.ReadTotallPublishedArticlesCount()
            };
            return View(viewModel);
        }
    }
}