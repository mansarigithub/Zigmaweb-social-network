using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.UI.ViewModels.Article;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.UI.Infrastructure.SignalR;

namespace ZigmaWeb.UI.Controllers
{
    public class ArticleController : BaseController
    {
        public ArticleService ArticleService { get; set; }
        public CommentService CommentService { get; set; }

        public ArticleController()
        {
            ArticleService = new ArticleService();
            CommentService = new CommentService(SignalRPushNotificationProvider.Instance);
        }

        [Route("article/{id:int}/{flag?}", Name = "ViewArticle")]
        public ActionResult Index(int id, string flag)
        {
            var previewMode = flag == "preview";
            ViewArticleModelContainer container;
            if (previewMode)
                container = ArticleService.ReadArticleForPreview(id);
            else
                container = ArticleService.ReadArticleForViewByVisitor(id);

            return View(new ArticleViewModel(container, previewMode));
        }

        public ActionResult Preview()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(CommentRegistrationPM comment)
        {
            CommentService.AddComment(comment, User);
            return Json(new { Id = comment.Id, CreateDate = comment.CreateDate.Value.ToPersianDate("g") });
        }
    }
}