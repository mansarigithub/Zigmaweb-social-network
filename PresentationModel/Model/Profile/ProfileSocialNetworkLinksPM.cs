using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ProfileSocialNetworkLinksPM
    {
        [LocalizedName]
        [Url(ErrorMessageResourceName = "InvalidUrl", ErrorMessageResourceType = typeof(Resources))]
        public string FacebookUrl { get; set; }

        [LocalizedName]
        [Url(ErrorMessageResourceName = "InvalidUrl", ErrorMessageResourceType = typeof(Resources))]
        public string LinkedInUrl { get; set; }

        [LocalizedName]
        [Url(ErrorMessageResourceName = "InvalidUrl", ErrorMessageResourceType = typeof(Resources))]
        public string TwitterUrl { get; set; }
    }
}
