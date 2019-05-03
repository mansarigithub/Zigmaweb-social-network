using System;
using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.UI.ViewModels.PasswordReset
{
    public class PasswordResetViewModel
    {
        [RequiredField]
        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Email { get; set; }

        public bool ShowSuccessMessage { get; set; }
    }
}