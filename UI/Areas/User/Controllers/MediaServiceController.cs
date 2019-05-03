using System;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Mvc;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;
using ZigmaWeb.UI.Infrastructure.StaticResource;
using System.Web;
using System.Net;
using ZigmaWeb.Common.Data;
using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model;
using System.IO;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class MediaServiceController : BaseController
    {
        public FileService FileService { get; set; }

        public MediaServiceController()
        {
            FileService = new FileService();
        }

        [HttpPost]
        public ActionResult SaveMedia(HttpPostedFileBase file)
        {
            var media = FileService.SaveMedia(file.InputStream, file.ContentLength, file.FileName, file.ContentType, User.UserId);
            return Json(new {
                Url = $"http://zigmaweb.ir/usermedia/{media.FileName}",
                embedHtml = ""
            });
        }

        [HttpGet]
        public ActionResult FileExist(string url)
        {
            bool fileExist;
            var request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "HEAD";
            try {
                var response = request.GetResponse() as HttpWebResponse;
                fileExist = response.StatusCode == HttpStatusCode.OK;
            }
            catch {
                fileExist = false;
            }
            return Json(fileExist, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult ReadUserMedia(DataSourceRequest request)
        {
            return Json(FileService.ReadUserMediaList(request, User), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteMedia(int id)
        {
            FileService.DeleteUserMedia(id, User);
            return Json(true);
        }
    }
}