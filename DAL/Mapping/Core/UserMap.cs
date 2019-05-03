using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.FirstName)
                .IsOptional()
                .HasMaxLength(25)
                .HasColumnName("FirstName");

            this.Property(t => t.LastName)
                .IsOptional()
                .HasMaxLength(25)
                .HasColumnName("LastName");

            this.Property(t => t.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("Email");

            this.Property(t => t.Sexuality)
                .IsOptional();

            this.Property(t => t.BirthDate)
                .HasColumnType("date")
                .IsOptional();

            // Table Mapping
            this.ToTable("User", "core");


            // Relationships
            this.HasRequired(t => t.Membership)
                .WithRequiredPrincipal(t => t.User);

            this.HasMany(t => t.Roles)
                .WithMany(t => t.Users)
                .Map(m =>
                {
                    m.ToTable("UserRole", "core");
                    m.MapLeftKey("UserId");
                    m.MapRightKey("RoleId");
                });
        }
    }
}
