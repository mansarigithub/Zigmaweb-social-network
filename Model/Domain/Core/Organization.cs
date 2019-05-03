using System.Collections.Generic;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Organization
    {
        public Organization()
        {
            EducationalResumes = new List<EducationalResume>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public OrganizationType Type { get; set; }

        public virtual ICollection<EducationalResume> EducationalResumes { get; set; }
        public virtual ICollection<JobResume> JobResumes { get; set; }
    }
}
