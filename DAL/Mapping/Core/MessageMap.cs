using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class MessageMap : EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.CreateDate)
                .HasColumnType("datetime2")
                .HasPrecision(2)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Message", "core");

            // Relationships
            this.HasRequired(t => t.Sender)
                .WithMany(t => t.SentMessages)
                .HasForeignKey(d => d.SenderId)
                .WillCascadeOnDelete(false);

            this.HasRequired(t => t.Receiver)
                .WithMany(t => t.ReceivedMessages)
                .HasForeignKey(d => d.ReceiverId)
                .WillCascadeOnDelete(false);
        }
    }
}
