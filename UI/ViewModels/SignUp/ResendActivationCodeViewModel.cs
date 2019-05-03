using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.UI.ViewModels.SignUp
{
    public class ResendActivationCodeViewModel
    {
        [RequiredField]
        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Email { get; set; }

        public bool ShowSuccessMessage { get; set; }
    }
}