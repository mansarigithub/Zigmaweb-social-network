using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.UI.ViewModels.PasswordReset;
using ZigmaWeb.Validation.Attributes;

namespace ZigmaWeb.UI.Controllers
{
    [ValidateModel]
    public class PasswordResetController : Controller
    {
        public UserRegistrationService UserRegistrationService { get; set; }

        public PasswordResetController()
        {
            UserRegistrationService = new UserRegistrationService();
        }

        [HttpGet]
        [Route("passwordreset")]
        public ActionResult Index()
        {
            var viewModel = new PasswordResetViewModel();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("passwordreset")]
        public ActionResult Index(PasswordResetViewModel viewModel)
        {
            UserRegistrationService.SendPasswordResetEmail(viewModel.Email);
            viewModel.Email = "";
            viewModel.ShowSuccessMessage = true;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult SetNew(int u, Guid t)
        {
            var viewModel = new SetNewPasswordViewModel() {
                UserId = u,
                PasswordResetToken = t,
                IsPasswordResetTokenValid = UserRegistrationService.IsPasswordResetTokenValid(u, t)
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNew(SetNewPasswordViewModel viewModel)
        {
            UserRegistrationService.ResetPassword(viewModel.UserId, viewModel.PasswordResetToken, viewModel.Password);
            return RedirectToAction("passwordchanged");
        }

        [HttpGet]
        public ActionResult PasswordChanged()
        {
            return View();
        }
    }
}