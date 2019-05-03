using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Media
    {
        public Media()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public int? ContentId { get; set; }
        public string FileName { get; set; }
        public int Size { get; set; }
        public DateTime? CreateDate { get; set; }
        public MediaType Type { get; set; }
        public User User { get; set; }
        public Content Content { get; set; }
    }
}
