using System;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ContentInfo5PM
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string Title { get; set; }

        public bool Published { get; set; }

        [ScriptIgnore]
        public DateTime? PublishDate { get; set; }
        
        [ScriptIgnore]
        public DateTime CreateDate { get; set; }

        public string PublishDateString => PublishDate.HasValue ? PublishDate.Value.ToPersianDate() : "";

        public string CreateDateString => CreateDate.ToPersianDate();

        public int CommentsCount { get; set; }
    }
}
