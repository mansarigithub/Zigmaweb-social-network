using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model.SetupBlog
{
    public class BlogGeneralSettingsPM
    {
        [RequiredField]
        [LocalizedName("BlogAddressWithStar")]
        [RegularExpression(@"^[a-zA-Z]{4,20}$", ErrorMessageResourceName = "InvalidBlogName", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Name { get; set; }

        [RequiredField]
        [LocalizedName("BlogTitleWithStar")]
        [StringLengthRange(5, 50)]
        public string Title { get; set; }

        [LocalizedName("AboutWeblog")]
        [StringLengthRange(0, 2000)]
        public string Description { get; set; }

        [LocalizedName]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmailAddress", ErrorMessageResourceType = (typeof(Localization.Resources)))]
        public string Email { get; set; }
    }
}
