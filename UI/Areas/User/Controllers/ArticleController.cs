using Common.Web.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UI.Areas.User.ViewModels.Article;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class ArticleController : BaseController
    {
        public ArticleService ArticleService { get; set; }
        public ContentService ContentService { get; set; }

        public ArticleController()
        {
            ArticleService = new ArticleService();
            ContentService = new ContentService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadUserContents(DataSourceRequest request)
        {
            return Json(ArticleService.ReadUserArticles(User, request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            //Response.AddHeader("Access-Control-Allow-Origin", "http://storage.zigmaweb.com");
            var viewModel = new EditArticleViewModel() {
                Content = new ContentRegistrationPM() {
                    Published = false,
                },
                Tags = new List<string>()
            };
            return View("Edit", viewModel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            ContentRegistrationPM content;
            List<string> tags = null;
            if (!id.HasValue) return Add();

            ArticleService.ReadArticleForEdit(User, id.Value, out content, out tags);
            var viewModel = new EditArticleViewModel() { Content = content, Tags = tags };
            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Save(EditArticleViewModel viewModel)
        {
            if (viewModel.Content.Id.HasValue) {
                ArticleService.EditArticle(viewModel.Content, viewModel.Tags);
            }
            else {
                viewModel.Content.AuthorId = User.UserId;
                ArticleService.AddArticle(viewModel.Content, viewModel.Tags);
            }

            return Json(new ProcessResult(new { ContentId = viewModel.Content.Id }));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> ids)
        {
            // ContentService.ChangeContentsState(ids, ContentState.Trashed, User);
            ContentService.RemoveContents(ids, User);
            return Json(true);
        }
    }
}