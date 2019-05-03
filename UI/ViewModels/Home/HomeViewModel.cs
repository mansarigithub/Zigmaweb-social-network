using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.Home
{
    public class HomeViewModel
    {
        public HomeViewModel(VisitorHomePageModelContainer container)
        {
            TopArticles = container.TopArticles;
            FeaturedArticles = container.FeaturedArticles;
            LatestArticles = container.LatestArticles;
            TopBlogPosts = container.TopBlogPosts;
            TotalArticles = container.TotalArticles;
            TotalBlogPosts = container.TotalBlogPosts;
        }

        public IEnumerable<ContentInfo6PM> TopArticles { get; set; }
        public IEnumerable<ContentInfo6PM> FeaturedArticles { get; set; }
        public IEnumerable<ContentInfo6PM> LatestArticles{ get; set; }
        public IEnumerable<ContentInfo6PM> TopBlogPosts { get; set; }
        public int TotalArticles { get; set; }
        public int TotalBlogPosts { get; set; }
    }
}