using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ForbiddenIdentifierMap : EntityTypeConfiguration<ForbiddenIdentifier>
    {
        public ForbiddenIdentifierMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Identifier)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("ForbiddenIdentifier", "core");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Identifier).HasColumnName("Identifier");
        }
    }
}
