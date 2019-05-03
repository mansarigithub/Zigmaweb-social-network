using System;
using System.Collections.Generic;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ContentInfo3PM
    {
        public int Id { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public string Title { get; set; }
        public string ShortAbstract { get; set; }
        public DateTime CreateDate { get; set; }
        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";
        public string CreateDateString => CreateDate.ToPersianDate();
        public IEnumerable<TagPM> Tags { get; set; }
    }
}
