using System;
using System.Collections.Generic;
using System.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Content
    {
        public Content()
        {
            this.Comments = new List<Comment>();
            this.Visits = new List<Visit>();
            this.Tags = new List<Tag>();
        }

        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int? PublicationId { get; set; }
        public ContentType Type { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string ShortAbstract { get; set; }
        public int CultureLcid { get; set; }
        public ContentState State { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastModifyDate { get; set; }
        public DateTime? PublishDate { get; set; }
        public string MetaDescription { get; set; }
        public short PersianCreateDateYear { get; set; }
        public byte PersianCreateDateMonth { get; set; }

        public virtual User Author { get; set; }
        public virtual Publication Publication { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Visit> Visits { get; set; }
        public virtual ICollection<Reference> References { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
        public virtual ICollection<FeaturedContent> FeaturedContents { get; set; }
    }
}
