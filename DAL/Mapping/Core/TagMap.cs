using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class TagMap : EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(25);

            // Table & Column Mappings
            this.ToTable("Tag", "core");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Text).HasColumnName("Text");

            // Relationships
            this.HasMany(t => t.Contents)
                .WithMany(t => t.Tags)
                .Map(m =>
                {
                    m.ToTable("ContentTag", "core");
                    m.MapLeftKey("TagId");
                    m.MapRightKey("ContentId");
                });
        }
    }
}
