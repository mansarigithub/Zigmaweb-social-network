using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ProfileGeneralSettingsPM
    {
        [RequiredField]
        [LocalizedName]
        [StringLengthRange(2, 25)]
        public string FirstName { get; set; }

        [RequiredField]
        [LocalizedName]
        [StringLengthRange(2, 25)]
        public string LastName { get; set; }

        [LocalizedName]
        public string AboutMe { get; set; }

        [LocalizedName]
        [Url(ErrorMessageResourceName = "InvalidUrl", ErrorMessageResourceType = typeof(Resources))]
        public string WebSiteUrl { get; set; }

        [LocalizedName]
        public string MobileNumber { get; set; }

        public DateTime? BirthDate { set; get; }

        [LocalizedName]
        public Sexuality? Sexuality { get; set; }

        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Email { get; set; }
    }
}
