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
using ZigmaWeb.UI.Areas.User.ViewModels.Blog;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;
using ZigmaWeb.UI.Infrastructure.SignalR;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class BlogController : BaseController
    {
        public BlogService BlogService { get; set; }
        public ContentService ContentService { get; set; }

        public BlogController()
        {
            BlogService = new BlogService(SignalRPushNotificationProvider.Instance);
            ContentService = new ContentService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadUserPosts(DataSourceRequest request)
        {
            return Json(BlogService.ReadUserPosts(User, request), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Add()
        {
            var viewModel = new EditPostViewModel() {
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

            BlogService.ReadPostForEdit(User, id.Value, out content, out tags);
            var viewModel = new EditPostViewModel() { Content = content, Tags = tags };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(EditPostViewModel viewModel)
        {
            if (viewModel.Content.Id.HasValue) {
                BlogService.EditPost(viewModel.Content, viewModel.Tags);
            }
            else {
                viewModel.Content.AuthorId = User.UserId;
                BlogService.AddPost(User.Blogs.First().Id, viewModel.Content, viewModel.Tags);
            }

            return Json(new ProcessResult(new { ContentId = viewModel.Content.Id }));
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> ids)
        {
            ContentService.ChangeContentsState(ids, ContentState.Trashed, User);
            return Json(true);
        }

    }
}