using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class FeaturedContentMap : EntityTypeConfiguration<FeaturedContent>
    {
        public FeaturedContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            // Table & Column Mappings
            this.ToTable("FeaturedContent", "core");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.FeaturedContents)
                .HasForeignKey(d => d.ContentId);
        }
    }
}
