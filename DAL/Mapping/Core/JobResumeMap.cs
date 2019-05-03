using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class JobResumeMap : EntityTypeConfiguration<JobResume>
    {
        public JobResumeMap()
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


            // Table & Column Mappings
            this.ToTable("JobResume", "core");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.JobResumes)
                .HasForeignKey(d => d.UserId);

            this.HasRequired(t => t.Organization)
                .WithMany(t => t.JobResumes)
                .HasForeignKey(d => d.OrganizationId);

            this.HasOptional(t => t.Job)
                .WithMany(t => t.JobResumes)
                .HasForeignKey(d => d.JobId);
        }
    }
}
