using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.PresentationModel.Model;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class VisitorHomePageModelContainer
    {
        public IEnumerable<ContentInfo6PM> TopArticles { get; set; }
        public IEnumerable<ContentInfo6PM> FeaturedArticles { get; set; }
        public IEnumerable<ContentInfo6PM> LatestArticles { get; set; }
        public IEnumerable<ContentInfo6PM> TopBlogPosts { get; set; }
        public int TotalArticles { get; set; }
        public int TotalBlogPosts { get; set; }
    }
}
