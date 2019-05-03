using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model.Content
{
    public class ContentInfoWithTextPM
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int AuthorId { get; set; }

        [ScriptIgnore]
        public string AuthorFirstName { get; set; }

        [ScriptIgnore]
        public string AuthorLastName { get; set; }

        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";

        public string Title { get; set; }

        [ScriptIgnore]
        public DateTime? PublishDate { get; set; }

        [ScriptIgnore]
        public DateTime CreateDate { get; set; }

        public string PublishDateString => PublishDate.HasValue ? PublishDate.Value.ToPersianDate() : "";

        public string CreateDateString => CreateDate.ToPersianDate("f");

        public int CommentsCount { get; set; }
        public IEnumerable<TagPM> Tags { get; set; }
    }
}
