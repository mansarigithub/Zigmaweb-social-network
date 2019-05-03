using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model.Blog
{
    public class BlogInfoForGenerateLinkPM
    {
        public string Title { get; set; }
        public string Name { get; set; }
    }
}
