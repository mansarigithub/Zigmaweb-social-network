using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model.Content
{
    public class UserInfoPm
    {
        public int Id { get; set; }

        [ScriptIgnore]
        public string FirstName { get; set; }

        [ScriptIgnore]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public bool IsApproved { get; set; }

        public bool IsLockedOut { get; set; }

        [ScriptIgnore]
        public DateTime? CreateDate { get; set; }

        public string RegisterDateString => CreateDate.HasValue ? CreateDate.Value.ToPersianDate() : "";

        [ScriptIgnore]
        public DateTime? LastLoginDate { get; set; }

        public string LastLoginDateString => LastLoginDate.HasValue ? LastLoginDate.Value.ToPersianDate() : "";
    }
}
