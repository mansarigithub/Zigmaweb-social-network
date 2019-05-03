using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class FollowMap : EntityTypeConfiguration<Follow>
    {
        public FollowMap()
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
            this.ToTable("Follow", "core");

            // Relationships
            this.HasRequired(t => t.Follower)
                .WithMany(t => t.Follows)
                .HasForeignKey(d => d.FollowerId);

            this.HasRequired(t => t.Followed)
                .WithMany(t => t.Followers)
                .HasForeignKey(d => d.FollowedId)
                .WillCascadeOnDelete(false);
        }
    }
}
