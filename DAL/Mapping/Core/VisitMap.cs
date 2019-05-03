using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class VisitMap : EntityTypeConfiguration<Visit>
    {
        public VisitMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Count)
                .IsRequired()
                .HasColumnName("Count");

            this.Property(t => t.Date)
                .IsRequired()
                .HasColumnName("Date")
                .HasColumnType("date");

            // Table Mapping
            this.ToTable("Visit", "core");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.Visits)
                .HasForeignKey(d => d.ContentId);
        }
    }
}
