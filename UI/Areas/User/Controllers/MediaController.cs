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
    public class MediaController : BaseController
    {
        public FileService FileService { get; set; }

        public MediaController()
        {
            FileService = new FileService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ReadUserMedia(DataSourceRequest request)
        {
            return Json(FileService.ReadUserMediaList(request, User), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            FileService.DeleteUserMedia(id, User);
            return Json(true);
        }
    }
}