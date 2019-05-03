using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class AdminDashboardModelContainer
    { 
        public int TotalUsersCount { get; set; }
        public int TotalArticlesCount { get; set; }
        public int TotalBlogPostCount { get; set; }
        public int TodayVisitsCount { get; set; }
        public IEnumerable<AdminDashboardNewUserPm> NewUsers { get; set; }
        public IEnumerable<AdminDashboardNewArticlePm> NewArticles { get; set; }
    }
}
