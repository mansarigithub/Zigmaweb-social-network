using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ContentMap : EntityTypeConfiguration<Content>
    {
        public ContentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Text)
                .IsRequired();

            this.Property(t => t.CultureLcid)
                .IsRequired();

            this.Property(t => t.State)
                .IsRequired();

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.LastModifyDate)
                .IsOptional();

            this.Property(t => t.LastModifyDate)
                .IsOptional();

            this.Property(t => t.PublishDate)
                .IsOptional();

            this.Property(t => t.MetaDescription)
                .IsOptional();

            this.Property(t => t.ShortAbstract)
                .HasMaxLength(250)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("Content", "core");

            // Relationships
            this.HasRequired(t => t.Author)
                .WithMany(t => t.Contents)
                .HasForeignKey(d => d.AuthorId);

            this.HasOptional(t => t.Publication)
                .WithMany(t => t.Contents)
                .HasForeignKey(d => d.PublicationId);
        }
    }
}
