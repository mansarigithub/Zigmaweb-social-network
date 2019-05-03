using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class MembershipMap : EntityTypeConfiguration<Membership>
    {
        public MembershipMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasColumnName("Id");

            this.Property(t => t.Password)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(32)
                .HasColumnName("Password");

            this.Property(t => t.FailedPasswordAttemptCount)
                .IsRequired()
                .HasColumnName("FailedPasswordAttemptCount");

            this.Property(t => t.PasswordResetToken)
                .IsOptional();

            // Table Mapping
            this.ToTable("Membership", "core");

        }
    }
}
