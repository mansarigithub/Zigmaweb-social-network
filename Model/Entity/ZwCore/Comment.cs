using System;
using System.Collections.Generic;
using ZigmaWeb.Model.Enum;

namespace ZigmaWeb.Model.Entity.ZwCore
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public string Text { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsPrivate { get; set; }
        public CommentStatus Status { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SenderWebSite { get; set; }
        public virtual Content Content { get; set; }
    }
}
