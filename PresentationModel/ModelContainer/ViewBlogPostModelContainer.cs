using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Archive;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model.Content;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class ViewBlogPostModelContainer
    {
        public ContentInfoWithTextPM Post { set; get; }
        public List<CommentInfoPM> Comments { set; get; }
        public List<TagPM> Tags { set; get; }
        public BlogInfoPM BlogInfo { get; set; }
        public List<BlogLinkPM> Links { get; set; }
        public IEnumerable<ContentArchiveItemPM> ArchiveItems { get; set; }
        public IEnumerable<ContentInfoWithTextPM> LatestPosts { get; set; }

    }
}
