using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.Areas.User.ViewModels.AdminDashboard
{
    public class AdminDashboardViewModel
    {
        public AdminDashboardViewModel(AdminDashboardModelContainer container)
        {
            TotalUsersCount = container.TotalUsersCount;
            TotalArticlesCount = container.TotalArticlesCount;
            TotalBlogPostCount = container.TotalBlogPostCount;
            TodayVisitsCount = container.TodayVisitsCount;
            NewArticles = container.NewArticles;
            NewUsers = container.NewUsers;
        }

        public int TotalUsersCount { get; set; }
        public int TotalArticlesCount { get; set; }
        public int TotalBlogPostCount { get; set; }
        public int TodayVisitsCount { get; set; }

        public IEnumerable<AdminDashboardNewUserPm> NewUsers { get; set; }
        public IEnumerable<AdminDashboardNewArticlePm> NewArticles { get; set; }
    }
}