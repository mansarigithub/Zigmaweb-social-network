using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class CommentMap : EntityTypeConfiguration<Comment>
    {
        public CommentMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Text)
                .IsRequired()
                .HasMaxLength(4000);

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.IsPrivate)
                .IsRequired();

            this.Property(t => t.Status)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Comment", "core");

            // Relationships
            this.HasRequired(t => t.Content)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.ContentId);

            this.HasRequired(t => t.Sender)
                .WithMany(t => t.Comments)
                .HasForeignKey(d => d.SenderId)
                .WillCascadeOnDelete(false);

        }
    }
}
