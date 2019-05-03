using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class EducationalResume
    {
        public EducationalResume()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int OrganizationId { get; set; }
        public int UniversityFieldId { get; set; }
        public EducationGrade EducationGrade { get; set; }
        public short? StartYear { get; set; }
        public short? EndYear { get; set; }

        public virtual User User { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual UniversityField UniversityField { get; set; }

    }
}
