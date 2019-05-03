using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class DashboardModelContainer
    {
        public int ArticlesCount { get; set; }
        public int BlogPostsCount { get; set; }
        public int TodayTotallVisits { get; set; }
        public int PrivateComments { get; set; }

        public List<CommentInfoPM> PendingComments { get; set; }
        public List<MessagePM> LatestMessages { get; set; }

    }
}
