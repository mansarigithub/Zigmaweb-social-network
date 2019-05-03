using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class UserRegistrationPM
    {
        [RequiredField]
        [LocalizedName]
        [StringLengthRange(2, 25)]
        public string FirstName { get; set; }

        [RequiredField]
        [LocalizedName]
        [StringLengthRange(2, 25)]
        public string LastName { get; set; }

        [RequiredField]
        [LocalizedName]
        [StringLengthRange(5,25)]
        public string Password { get; set; }

        [RequiredField]
        [LocalizedName]
        [Compare("Password", ErrorMessageResourceName = "PasswordAndPasswordConfirmMismatch", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string PasswordConfirm { get; set; }

        [RequiredField]
        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType =(typeof(Localization.Resources)))]
        public string Email { get; set; }
    }
}
