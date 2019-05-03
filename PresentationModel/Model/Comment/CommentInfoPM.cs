using System;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.PresentationModel.Model
{
    public class CommentInfoPM
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsPrivate { get; set; }
        public string ContentTitle { get; set; }
        public CommentStatus Status { get; set; }
        public int SenderId { get; set; }

        [ScriptIgnore]
        public string SenderFirstName { get; set; }

        [ScriptIgnore]
        public string SenderLastName { get; set; }

        [ScriptIgnore]
        public DateTime CreateDate { get; set; }


        public string SenderFullName => $"{SenderFirstName} {SenderLastName}";

        public string CreateDateString => CreateDate.ToPersianDate("g");
    }
}
