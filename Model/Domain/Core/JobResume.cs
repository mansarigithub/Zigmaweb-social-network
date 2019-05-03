using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class JobResume
    {
        public JobResume()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? JobId { get; set; }
        public int OrganizationId { get; set; }
        public short? StartYear { get; set; }
        public short? EndYear { get; set; }

        public virtual Job Job { get; set; }
        public virtual User User { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
