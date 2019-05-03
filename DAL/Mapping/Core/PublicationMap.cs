using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class PublicationMap : EntityTypeConfiguration<Publication>
    {
        public PublicationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name)
                .IsOptional()
                .HasMaxLength(30);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateDate)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Publication", "core");

            // Relationships
            this.HasRequired(t => t.Creator)
                .WithMany(t => t.Publications)
                .HasForeignKey(d => d.CreatorId);
        }
    }
}
