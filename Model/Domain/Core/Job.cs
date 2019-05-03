using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Job
    {
        public Job()
        {
            this.JobResumes = new List<JobResume>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<JobResume> JobResumes { get; set; }
    }
}
