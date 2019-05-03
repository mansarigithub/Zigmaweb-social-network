using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class MediaMap : EntityTypeConfiguration<Media>
    {
        public MediaMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.FileName)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CreateDate)
                .IsOptional()
                .HasColumnType("datetime2")
                .HasPrecision(2);


            // Table & Column Mappings
            this.ToTable("Media", "core");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.MediaFiles)
                .HasForeignKey(d => d.UserId);

            this.HasOptional(t => t.Content)
               .WithMany(t => t.Medias)
               .HasForeignKey(d => d.ContentId);
        }
    }
}
