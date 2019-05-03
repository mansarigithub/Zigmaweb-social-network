using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.PresentationModel.ModelContainer.Blog;

namespace ZigmaWeb.UI.ViewModels.Blog
{
    public class BlogPostViewModel : BlogPageViewModelBase
    {
        public ContentInfoWithTextPM Post { set; get; }
        public List<CommentInfoPM> Comments { set; get; }
        public List<TagPM> Tags { set; get; }
        public bool PreviewMode { get; set; }

        public BlogPostViewModel(ViewBlogPostModelContainer container, bool previewMode)
        {
            Post = container.Post;
            Comments = container.Comments ?? new List<CommentInfoPM>();
            Tags = container.Tags ?? new List<TagPM>();
            BlogLinks = container.Links ?? new List<BlogLinkPM>();
            PreviewMode = previewMode;
            PublicationId = container.BlogInfo.Id;
            CreatorFirstName = container.BlogInfo.CreatorFirstName;
            CreatorId = container.BlogInfo.CreatorId;
            BlogDescription = container.BlogInfo.Description;
            BlogEmail = container.BlogInfo.Email;
            Name = container.BlogInfo.Name;
            Title = container.BlogInfo.Title;
            ArchiveItems = container.ArchiveItems;
            BlogLatestPosts = container.LatestPosts;
        }
    }
}