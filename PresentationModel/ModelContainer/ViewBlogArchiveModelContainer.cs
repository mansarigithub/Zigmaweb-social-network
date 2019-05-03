using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Archive;
using ZigmaWeb.PresentationModel.Model.Blog;

namespace ZigmaWeb.PresentationModel.ModelContainer
{
    public class ViewBlogArchiveModelContainer
    {
        public BlogInfoPM BlogInfo { get; set; }
        public List<BlogLinkPM> Links { get; set; }
        public IEnumerable<ContentInfo2PM> PostTitles { get; set; }
        public IEnumerable<ContentArchiveItemPM> ArchiveItems { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }
    }
}
