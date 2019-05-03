using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Entity.Membership
{
    public partial class BinaryProfile
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Key { get; set; }
        public byte[] Value { get; set; }
        public virtual User User { get; set; }
    }
}
