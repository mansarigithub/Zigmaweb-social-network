using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using ZigmaWeb.Common.Globalization;

namespace ZigmaWeb.PresentationModel.Model.Content
{
    public class AdminDashboardNewArticlePm
    {
        public int Id { get; set; }

        public int AuthorId { get; set; }
        [ScriptIgnore]
        public string AuthorFirstName { get; set; }

        [ScriptIgnore]
        public string AuthorLastName { get; set; }

        public string AuthorFullName => $"{AuthorFirstName} {AuthorLastName}";

        public string AuthorImageUrl { get; set; }
        public string Title { get; set; }

        [ScriptIgnore]
        public DateTime? PublishDate { get; set; }


        public string PublishDateString => PublishDate.HasValue ? PublishDate.Value.ToPersianDate() : "";

    }
}
