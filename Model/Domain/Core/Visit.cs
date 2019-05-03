using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Visit
    {
        public Visit()
        {
        }

        public int Id { get; set; }
        public int ContentId { get; set; }
        public int Count { get; set; }
        public DateTime Date { get; set; }

        public virtual Content Content { get; set; }
    }
}
