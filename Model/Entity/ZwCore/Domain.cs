using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Entity.ZwCore
{
    public partial class Domain
    {
        public Domain()
        {
            this.SourceContents = new List<SourceContent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<SourceContent> SourceContents { get; set; }
    }
}
