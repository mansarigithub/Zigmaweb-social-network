using System;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Rate
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int UserId { get; set; }
        public byte Value { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual Content Content { get; set; }
        public virtual User User { get; set; }
    }
}
