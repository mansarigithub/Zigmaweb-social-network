using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ProfileKeyValueMap : EntityTypeConfiguration<ProfileKeyValue>
    {
        public ProfileKeyValueMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Type)
                .IsRequired();

            this.Property(t => t.Value)
                .HasMaxLength(4000)
                .IsRequired();


            // Table Mapping
            this.ToTable("Profile", "core");

            // Relationships
            this.HasRequired(t => t.User)
                .WithMany(t => t.ProfileKeyValues)
                .HasForeignKey(d => d.UserId);
        }
    }
}
