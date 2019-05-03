using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using ZigmaWeb.Model.Domain.Core;

namespace ZigmaWeb.DataAccess.Mapping.Core
{
    public class ChannelMap : EntityTypeConfiguration<Channel>
    {
        public ChannelMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            // Table & Column Mappings
            this.ToTable("Channel", "core");

            // Relationships
        }
    }
}
