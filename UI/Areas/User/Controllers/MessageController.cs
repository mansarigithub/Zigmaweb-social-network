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
    public class MessageController : BaseController
    {
        public MessageService MessageService { get; set; }
        public CommentService CommentManagement { get; set; }

        public MessageController()
        {
            MessageService = new MessageService(SignalRPushNotificationProvider.Instance);
            CommentManagement = new CommentService(SignalRPushNotificationProvider.Instance);
        }

        public ActionResult Index()
        {
            return View("Received");
        }

        public ActionResult Sent()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadReceivedMessages(DataSourceRequest request)
        {
            return Json(MessageService.ReadReceivedMessages(request, User), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReadSentMessages(DataSourceRequest request)
        {
            return Json(MessageService.ReadSentMessages(request, User), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(IEnumerable<int> ids)
        {
            MessageService.DeleteMessages(ids, User);
            return Json(true);
        }
    }
}