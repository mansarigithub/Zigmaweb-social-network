using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Model.Entity.Membership;
using ZigmaWeb.Model.Enum;

namespace ZigmaWeb.Model.Entity.ZwCore
{
    public partial class Content
    {
        public Content()
        {
            this.Comments = new List<Comment>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public ContentType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int CultureLcid { get; set; }
        public bool Published { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastModifyDate { get; set; }
        public Nullable<System.DateTime> PublishDate { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeyWords { get; set; }
        public int VisitCount { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual SourceContent SourceContent { get; set; }

        public CultureInfo GetCultureInfo()
        {
            return CultureInfo.GetCultureInfo(CultureLcid);
        }
    }
}
