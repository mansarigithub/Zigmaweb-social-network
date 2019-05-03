using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class NotificationServiceController : BaseController
    {
        public NotificationService NotificationService { get; set; }

        public NotificationServiceController()
        {
            NotificationService = new NotificationService();
        }

        [HttpGet]
        public ActionResult ReadNotifications()
        {
            return Json(NotificationService.ReadUserNotifications(User.UserId, 30), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SetNotificationAsRead(int id)
        {
            NotificationService.SetNotificationAsRead(User, id);
            return Json("", JsonRequestBehavior.DenyGet);
        }
    }
}