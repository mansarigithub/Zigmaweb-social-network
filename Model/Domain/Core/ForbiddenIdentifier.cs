using System;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class ForbiddenIdentifier 
    {
        public int Id { get; set; }
        public string Identifier { get; set; }
        public ForbiddenIdentifierContext Context { get; set; }
    }
}
