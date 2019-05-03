using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model.Content
{
    public class AdminDashboardNewUserPm
    {
        public int Id { get; set; }

        [ScriptIgnore]
        public string FirstName { get; set; }

        [ScriptIgnore]
        public string LastName { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string Email { get; set; }

        public string ImageUrl { get; set; }

        [ScriptIgnore]
        public DateTime? CreateDate { get; set; }

        public string RegisterDateString => CreateDate.HasValue ? CreateDate.Value.ToPersianDate() : "";

    }
}
