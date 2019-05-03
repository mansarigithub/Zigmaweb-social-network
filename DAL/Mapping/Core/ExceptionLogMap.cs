using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ExceptionLogMap : EntityTypeConfiguration<ExceptionLog>
    {
        public ExceptionLogMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Message)
                .HasMaxLength(4000)
                .IsRequired();

            this.Property(t => t.InnerMessage)
                .HasMaxLength(4000)
                .IsOptional();

            this.Property(t => t.HttpRequestUrl)
                .HasMaxLength(4000)
                .IsOptional();

            // Table & Column Mappings
            this.ToTable("ExceptionLog", "core");

            // Relationships
        }
    }
}
