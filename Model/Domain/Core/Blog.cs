using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Blog : Publication
    {
        public Blog()
        {
            Type = PublicationType.Blog;
            Links = new List<BlogLink>();
        }

        public string Description { get; set; }
        public string Email { get; set; }
        public virtual ICollection<BlogLink> Links { get; set; }
    }
}
