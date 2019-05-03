using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Archive;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.Model.SetupBlog;

namespace ZigmaWeb.PresentationModel.ModelContainer.Blog
{
    public class BlogHomePageModelContainer
    {
        public BlogInfoPM BlogInfo { get; set; }
        public IEnumerable<ContentInfoWithTextPM> LatestPosts { get; set; }
        public IEnumerable<BlogLinkPM> Links { get; set; }
        public IEnumerable<ContentArchiveItemPM> ArchiveItems { get; set; }
    }
}
