using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.Infrastructure.SignalR;
using ZigmaWeb.UI.ViewModels.UserHomePage;

namespace ZigmaWeb.UI.Controllers
{
    public class ProfileController : BaseController
    {
        public UserProfileService UserProfileService { get; set; }
        public MessageService MessageService { get; set; }

        public ProfileController()
        {
            UserProfileService = new UserProfileService();
            MessageService = new MessageService(SignalRPushNotificationProvider.Instance);
        }

        [Route("profile/{userId}")]
        public ActionResult Index(int userId)
        {
            var viewModel = new UserHomePageViewModel(UserProfileService.ReadUserInfoForProfilePage(userId, User));
            return View(viewModel);
        }

        [Route("profile/sendmessage")]
        public ActionResult SendMessage(int userId, string text)
        {
            MessageService.CreateMessage(User.UserId, userId, text);
            return Json(true);
        }

        [HttpPost]
        [Route("profile/followuser")]
        public ActionResult FollowUser(int userId)
        {
            UserProfileService.FollowUser(userId, User);
            return Json(true);
        }

        [HttpPost]
        [Route("profile/unfollowuser")]
        public ActionResult UnfollowUser(int userId)
        {
            UserProfileService.UnfollowUser(userId, User);
            return Json(true);
        }
    }
}