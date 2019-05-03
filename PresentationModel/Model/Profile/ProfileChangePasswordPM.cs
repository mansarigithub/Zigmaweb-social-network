using System;
using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ProfileChangePasswordPM
    {
        [RequiredField]
        [LocalizedName]
        [StringLengthRange(5, 25)]
        public string CurrentPassword { get; set; }

        [RequiredField]
        [LocalizedName]
        [StringLengthRange(5, 25)]
        public string Password { get; set; }

        [RequiredField]
        [LocalizedName]
        [Compare("Password", ErrorMessageResourceName = "PasswordAndPasswordConfirmMismatch", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string PasswordConfirm { get; set; }

    }
}
