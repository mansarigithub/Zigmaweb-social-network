using Common.Exception;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.IO;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.UI.Areas.User.ViewModels.Profile;
using ZigmaWeb.UI.Controllers;
using ZigmaWeb.Validation.Attributes;

namespace ZigmaWeb.UI.Areas.User.Controllers
{
    public class ProfileSettingsController : BaseController
    {
        public UserProfileService UserProfileService { get; set; }

        public ProfileSettingsController()
        {
            UserProfileService = new UserProfileService();
        }


        public ActionResult Index()
        {
            var urlsAndStats = UserProfileService.ReadProfileStatisticsAndSocialLinks(User.UserId);
            var viewModel = new ProfileSettingsViewModel() {
                GeneralSettings = UserProfileService.ReadProfileGeneralSettings(User.UserId),
                ProfileStatisticsAndSocialLinks = urlsAndStats,
                ProfileSocialNetworkUrls = new ProfileSocialNetworkLinksPM() {
                    LinkedInUrl = urlsAndStats.LinkedInUrl,
                    TwitterUrl = urlsAndStats.TwitterUrl,
                    FacebookUrl = urlsAndStats.FacebookUrl
                }
            };
            return View(viewModel);
        }

        #region General Settings
        public ActionResult UpdateGeneralSettings(ProfileSettingsViewModel viewModel)
        {
            UserProfileService.UpdateUserGeneralSettings(User, viewModel.GeneralSettings);
            return Json("");
        }
        #endregion

        #region Social Networks
        [ValidateModel]
        public ActionResult UpdateSocialNetworkUrls(ProfileSocialNetworkLinksPM profileSocialNetworkUrls)
        {
            UserProfileService.UpdateUserSocialNetworkUrls(User, profileSocialNetworkUrls);
            return Json("");
        }

        #endregion

        #region Security
        public ActionResult UpdatePassword(ProfileSettingsViewModel viewModel)
        {
            UserProfileService.UpdateUserPassword(User.UserId, viewModel.ChangePasswordPM.CurrentPassword, viewModel.ChangePasswordPM.Password);
            return Json("");
        }
        #endregion

        #region Profile Picture
        public ActionResult SaveProfilePicture(HttpPostedFileBase img)
        {
            int imageWidth, imageHeight;
            UserProfileService.SaveUserOriginalProfileImage(User, img.InputStream, out imageWidth, out imageHeight);
            img.InputStream.Dispose();
            return Json(new {
                status = "success",
                url = $"/user/profilesettings/readprofilepictureforcrop/{Guid.NewGuid()}",
                width = imageWidth,
                height = imageHeight
            });
        }

        [Route("user/profilesettings/readprofilepictureforcrop/{anything?}")]
        public ActionResult ReadProfilePictureForCrop(string anything)
        {
            string path, mime;
            UserProfileService.GetOriginalProfileImageFilePath(User.UserId, out path, out mime);
            return File(path, mime);
        }

        public ActionResult CropProfileImage(ProfileImageCropInfoPM cropInfo)
        {
            UserProfileService.CropUserProfileImages(User.UserId, cropInfo);
            return Json(new {
                status = "success",
                url = $"/user/service/getprofileimage/large/{Guid.NewGuid()}",
            });
        }
        #endregion
    }
}