using System;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Localization.Attribute;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class NotificationPM
    {
        public int Id { get; set; }

        public bool IsRead { get; set; }

        [ScriptIgnore]
        public DateTime CreateDate { get; set; }

        [ScriptIgnore]
        public NotificationType Type { get; set; }

        public string CreateDateString => CreateDate.ToPersianDate("g");

        public string Message { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}
