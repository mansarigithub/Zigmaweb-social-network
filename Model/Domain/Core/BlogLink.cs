using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class BlogLink 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public BlogLink()
        {
        }
    }
}
