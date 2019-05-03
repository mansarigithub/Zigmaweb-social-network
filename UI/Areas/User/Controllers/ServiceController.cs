using System;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.UI.Infrastructure.Mvc;
using ZigmaWeb.UI.Infrastructure.Filters.Authorization;
using ZigmaWeb.UI.Infrastructure.StaticResource;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    [AuthorizeUser]
    public class ServiceController : BaseController
    {
        public UserProfileService UserProfileService { get; set; }
        public NotificationService NotificationService { get; set; }
        public UserRegistrationService UserRegistrationService { get; }
        public ServiceController()
        {
            UserProfileService = new UserProfileService();
            NotificationService = new NotificationService();
            UserRegistrationService = new UserRegistrationService();
        }

        public ActionResult SignOut()
        {
            SessionManager.Abandon();
            CookieManager.RemoveAuthenticationCookie();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Route("user/service/getprofileimage/{imageSize}/{anything?}", Name = "GetProfileImage")]
        public ActionResult GetProfileImage(string imageSize, string anything)
        {
            ProfileImageSize profileImageSize;
            string mime, path;
            if (!Enum.TryParse<ProfileImageSize>(imageSize, true, out profileImageSize))
                profileImageSize = ProfileImageSize.Small;

            if (!UserProfileService.TryGetProfileImageFilePath(User.UserId, profileImageSize, out path, out mime)) {
                path = StaticResourceHelper.GetAnonymousUserIconPhysicalPath(profileImageSize);
                mime = "image/png";
            }

            return File(path, mime);
        }

        [HttpPost]
        public ActionResult ResendEmailConfirmationLink()
        {
            UserRegistrationService.ResendActivationEmail(User.Email);
            return Json(new object());
        }
    }
}