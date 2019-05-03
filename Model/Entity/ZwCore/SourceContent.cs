using System;
using System.Collections.Generic;
using System.Globalization;

namespace ZigmaWeb.Model.Entity.ZwCore
{
    public partial class SourceContent
    {
        public int Id { get; set; }
        public int CultureLcid { get; set; }
        public int DomainId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Summary { get; set; }
        public virtual Content Content { get; set; }
        public virtual Domain Domain { get; set; }

        public CultureInfo GetCultureInfo()
        {
            return CultureInfo.GetCultureInfo(CultureLcid);
        }
    }
}
