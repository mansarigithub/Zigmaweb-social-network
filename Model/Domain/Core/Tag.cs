using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Tag 
    {
        public Tag()
        {
            this.Contents = new List<Content>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
    }
}
