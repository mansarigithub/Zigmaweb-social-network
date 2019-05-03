using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class BlogMap : EntityTypeConfiguration<Blog>
    {
        public BlogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Email)
                .IsOptional()
                .HasMaxLength(50);

            this.Property(t => t.Description)
                .IsOptional()
                .HasMaxLength(4000);

            // Table & Column Mappings
            this.ToTable("Blog", "core");

            // Relationships
        }
    }
}
