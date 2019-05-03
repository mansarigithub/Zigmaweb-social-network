using System.Collections.Generic;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.Blog
{
    public class BlogArchiveViewModel : BlogPageViewModelBase
    {
        public IEnumerable<ContentInfo2PM> PostTitles { get; set; }
        public int? Year { get; set; }
        public int? Month { get; set; }

        public BlogArchiveViewModel(ViewBlogArchiveModelContainer container)
        {
            PostTitles = container.PostTitles;
            BlogLinks = container.Links ?? new List<BlogLinkPM>();
            PublicationId = container.BlogInfo.Id;
            CreatorFirstName = container.BlogInfo.CreatorFirstName;
            CreatorId = container.BlogInfo.CreatorId;
            BlogDescription = container.BlogInfo.Description;
            BlogEmail = container.BlogInfo.Email;
            Name = container.BlogInfo.Name;
            Title = container.BlogInfo.Title;
            ArchiveItems = container.ArchiveItems;
            Year = container.Year;
            Month = container.Month;
        }
    }
}