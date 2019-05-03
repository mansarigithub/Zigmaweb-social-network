using System;
using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.UI.ViewModels.SignUp
{
    public class AccountActivationViewModel
    {
        /// <summary>
        /// Verification Code
        /// </summary>
        public Guid VC { get; set; }

        /// <summary>
        /// User Id
        /// </summary>
        public int U { get; set; }
        public string ErrorMessage { get; set; }
    }
}