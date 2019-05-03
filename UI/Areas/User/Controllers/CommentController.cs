using System.Collections.Generic;
using System.Web.Mvc;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;
using ZigmaWeb.UI.Infrastructure.SignalR;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class CommentController : BaseController
    {
        public ArticleService ContentManagement { get; set; }
        public CommentService CommentManagement { get; set; }

        public CommentController()
        {
            ContentManagement = new ArticleService();
            CommentManagement = new CommentService(SignalRPushNotificationProvider.Instance);
        }

        public ActionResult Index(int? contentId)
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadComments(DataSourceRequest request)
        {
            return Json(CommentManagement.ReadCommentsForViewByOwner(request, User), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> ids)
        {
            CommentManagement.DeleteComments(ids, User);
            return Json(true);
        }

    }
}