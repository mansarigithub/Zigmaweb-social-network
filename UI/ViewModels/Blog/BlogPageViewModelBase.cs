using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model.Archive;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.UI.ViewModels.Publication;

namespace ZigmaWeb.UI.ViewModels.Blog
{
    public class BlogPageViewModelBase : PublicationViewModelBase
    {
        public string BlogDescription { get; set; }
        public string BlogEmail { get; set; }
        public IEnumerable<ContentInfoWithTextPM> BlogLatestPosts { get; set; }
        public IEnumerable<BlogLinkPM> BlogLinks { get; set; }
        public IEnumerable<ContentArchiveItemPM> ArchiveItems { get; set; }

    }
}