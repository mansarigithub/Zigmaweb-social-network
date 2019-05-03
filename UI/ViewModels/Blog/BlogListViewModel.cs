using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.Tag
{
    public class BlogListViewModel
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public IEnumerable<BlogInfoForGenerateLinkPM> Blogs { get; set; }
        public int TotallRows { get; set; }
    }
}