using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ReferenceMap : EntityTypeConfiguration<Reference>
    {
        public ReferenceMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.CultureLcid)
                .IsRequired();

            this.Property(t => t.Url)
                .IsOptional()
                .HasMaxLength(2048);

            // Table & Column Mappings
            this.ToTable("Reference", "core");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.CultureLcid).HasColumnName("CultureLcid");
            this.Property(t => t.Title).HasColumnName("Title");
            this.Property(t => t.Url).HasColumnName("Url");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.References)
                .HasForeignKey(t => t.ContentId);
        }
    }
}
