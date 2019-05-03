using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class EducationalResumeMap : EntityTypeConfiguration<EducationalResume>
    {
        public EducationalResumeMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.StartYear)
                .IsOptional();
            this.Property(t => t.EndYear)
                .IsOptional();
            this.Property(t => t.EducationGrade)
                .IsRequired();


            // Table & Column Mappings
            this.ToTable("EducationalResume", "core");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.EducationalResumes)
                .HasForeignKey(d => d.UserId);

            this.HasRequired(t => t.Organization)
                .WithMany(t => t.EducationalResumes)
                .HasForeignKey(d => d.OrganizationId);

            this.HasRequired(t => t.UniversityField)
                .WithMany(t => t.EducationalResumes)
                .HasForeignKey(d => d.UniversityFieldId);
        }
    }
}
