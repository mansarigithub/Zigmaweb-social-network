using ZigmaWeb.PresentationModel.ModelContainer.Blog;

namespace ZigmaWeb.UI.ViewModels.Blog
{
    public class BlogHomePageViewModel : BlogPageViewModelBase
    {
        public BlogHomePageViewModel(BlogHomePageModelContainer modelContainer)
        {
            this.PublicationId = modelContainer.BlogInfo.Id;
            this.CreatorFirstName = modelContainer.BlogInfo.CreatorFirstName;
            this.CreatorLastName = modelContainer.BlogInfo.CreatorLastName;
            this.CreatorId = modelContainer.BlogInfo.CreatorId;
            this.BlogDescription = modelContainer.BlogInfo.Description;
            this.BlogEmail = modelContainer.BlogInfo.Email;
            this.Name = modelContainer.BlogInfo.Name;
            this.Title = modelContainer.BlogInfo.Title;
            this.BlogLinks = modelContainer.Links;
            this.BlogLatestPosts = modelContainer.LatestPosts;
            this.ArchiveItems = modelContainer.ArchiveItems;
        }
    }
}