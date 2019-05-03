using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.UI.ViewModels.SignIn
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {
            RememberMe = true;
        }

        [RequiredField]
        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Email { get; set; }

        [RequiredField]
        [LocalizedName]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string ErrorMessage { get; set; }
    }
}