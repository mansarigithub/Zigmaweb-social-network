using Common.Exception;
using System.Web.Mvc;
using ZigmaWeb.Facade.Core;
using ZigmaWeb.Localization.ExtensionMethod;
using ZigmaWeb.UI.ViewModels.SignUp;
using ZigmaWeb.Validation.Attributes;

namespace ZigmaWeb.UI.Controllers
{
    public class SignUpController : Controller
    {
        public UserRegistrationService UserRegistrationService { get; set; }
        public SignUpController()
        {
            UserRegistrationService = new UserRegistrationService();
        }

        [Route("signup")]
        public ActionResult Index()
        {
            return View(new SignUpViewModel() { });
        }

        [Route("signup")]
        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SignUpViewModel viewModel)
        {
            try {
                //throw new BusinessException("RegistrationIsDisabledNow".Localize());
                UserRegistrationService.RegisterUser(viewModel.User, true);
            }
            catch (BusinessException exp) {
                viewModel.ErrorMessage = exp.Message;
                viewModel.User.Password = viewModel.User.PasswordConfirm = "";
                return View("index", viewModel);
            }
            return RedirectToAction("ActivateYourAccount");
        }

        [HttpGet]
        public ActionResult ActivateYourAccount()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ActivateAccount(AccountActivationViewModel viewModel)
        {
            try {
                UserRegistrationService.ActivateAccount(viewModel.U, viewModel.VC);
            }
            catch (BusinessException exp) {
                viewModel.ErrorMessage = exp.Message;
            }
            return View("AccountActivationResult", viewModel);
        }

        [HttpGet]
        public ActionResult ResendActivationCode()
        {
            var viewModel = new ResendActivationCodeViewModel() {
                ShowSuccessMessage = false
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateModel]
        [ValidateAntiForgeryToken]
        public ActionResult ResendActivationCode(ResendActivationCodeViewModel viewModel)
        {
            UserRegistrationService.ResendActivationEmail(viewModel.Email);
            viewModel.ShowSuccessMessage = true;
            viewModel.Email = "";
            return View(viewModel);
        }
    }
}