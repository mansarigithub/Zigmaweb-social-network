using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class RateMap : EntityTypeConfiguration<Rate>
    {
        public RateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.Value)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Rate", "core");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.Rates)
                .HasForeignKey(d => d.ContentId);

            this.HasRequired(t => t.User)
                .WithMany(t => t.Rates)
                .HasForeignKey(d => d.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
