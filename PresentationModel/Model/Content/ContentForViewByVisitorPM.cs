using System;
using System.Web.Mvc;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class ContentForViewByVisitorPM
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public bool Published { get; set; }
        public string ShortAbstract { get; set; }
        public string AuthorFullName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateString {
            get
            {
                return CreateDate.ToPersianDate();
            }
        }
    }
}
