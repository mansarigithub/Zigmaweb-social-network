using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class UniversityField
    {
        public UniversityField()
        {
            EducationalResumes = new List<EducationalResume>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<EducationalResume> EducationalResumes { get; set; }

    }
}
