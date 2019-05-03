using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Publication
    {
        public Publication()
        {
        }

        public int Id { get; set; }
        public PublicationType Type { get; set; }
        public int CreatorId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User Creator { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
