using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.UI.ViewModels.SignUp
{
    public class SignUpViewModel
    {
        public UserRegistrationPM User { get; set; }

        [EnforceTrue("MsgYouShouldAcceptLicenceTerms")]
        public bool IAcceptLicenceTerms { get; set; }

        public string ErrorMessage { get; set; }
    }
}