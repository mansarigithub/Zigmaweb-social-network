using System;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class ProfileKeyValue
    {
        public ProfileKeyValue()
        {
        }

        public ProfileKeyValue(ProfileKeyValueType type, string value)
        {
            Type = type;
            Value = value;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public ProfileKeyValueType Type { get; set; }
        public string Value { get; set; }

        public virtual User User { get; set; }
    }
}
