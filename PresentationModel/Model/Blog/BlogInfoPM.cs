using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Localization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model.Blog
{
    public class BlogInfoPM
    {
        public int Id { get; set; }
        public int CreatorId { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }

        public string CreatorFirstName { get; set; }
        public string CreatorLastName { get; set; }
        public string CreatorFullName {
            get
            {
                return $"{CreatorFirstName} {CreatorLastName}";
            }
        }
    }
}
