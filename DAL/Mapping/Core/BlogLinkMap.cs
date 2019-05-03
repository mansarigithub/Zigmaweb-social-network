using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class BlogLinkMap : EntityTypeConfiguration<BlogLink>
    {
        public BlogLinkMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Url)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("BlogLink", "core");

            // Relationships
            this.HasRequired(t => t.Blog)
                .WithMany(t => t.Links)
                .HasForeignKey(t => t.BlogId);
        }
    }
}
