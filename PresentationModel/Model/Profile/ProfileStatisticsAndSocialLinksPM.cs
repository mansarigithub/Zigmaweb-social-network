using System;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ProfileStatisticsAndSocialLinksPM
    {
        public string FacebookUrl { get; set; }
        public string LinkedInUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string WebSiteUrl { get; set; }
        public int TotalArticles { get; set; }
        public int TotalBlogPosts { get; set; }
        public int TotalVisits { get; set; }
    }
}
