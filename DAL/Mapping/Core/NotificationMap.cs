using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class NotificationMap : EntityTypeConfiguration<Notification>
    {
        public NotificationMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CreateDate)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.IsRead)
                .IsRequired();

            // Table Mapping
            this.ToTable("Notification", "core");

            // Relationships
            this.HasOptional(t => t.Subject)
                .WithMany(t => t.CausedNotifications)
                .HasForeignKey(d => d.SubjectId);

            this.HasRequired(t => t.Receiver)
                .WithMany(t => t.Notifications)
                .HasForeignKey(d => d.ReceiverId);
        }
    }
}
