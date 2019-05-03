using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Channel : Publication
    {
        public Channel()
        {
            Type = PublicationType.Channel;
        }
    }
}
