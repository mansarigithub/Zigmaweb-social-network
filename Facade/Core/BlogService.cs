using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.DataProvider;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.Web;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.Common.PushNotification;
using ZigmaWeb.PresentationModel.Model.Archive;

namespace ZigmaWeb.Facade.Core
{
    public class BlogService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        BlogBiz BlogBiz { get; set; }
        PublicationBiz PublicationBiz { get; set; }
        ContentBiz ContentBiz { get; set; }
        TagBiz TagBiz { get; set; }
        BlogLinkBiz FriendBiz { get; set; }
        VisitBiz VisitBiz { get; set; }
        IPushNotificationProvider PushNotificationProvider { get; set; }


        public BlogService(IPushNotificationProvider pushNotificationProvider)
        {
            UnitOfWork = new CoreUnitOfWork();
            BlogBiz = new BlogBiz(UnitOfWork);
            PublicationBiz = new PublicationBiz(UnitOfWork);
            TagBiz = new TagBiz(UnitOfWork);
            FriendBiz = new BlogLinkBiz(UnitOfWork);
            ContentBiz = new ContentBiz(UnitOfWork);
            VisitBiz = new VisitBiz(UnitOfWork);
            PushNotificationProvider = pushNotificationProvider;
        }

        #region Post CRUD

        public ViewBlogPostModelContainer ReadPostForViewByVisitor(string publicationName, int postId)
        {
            var content = BlogBiz.ReadPostForViewByVisitor(publicationName, postId);
            VisitBiz.IncrementContentVisits(content.Id);
            UnitOfWork.SaveChanges();
            return new ViewBlogPostModelContainer() {
                BlogInfo = BlogBiz.Read(b => b.Id == content.PublicationId).MapTo<BlogInfoPM>().Single(),
                Post = content.GetContentInfoWithTextPM(),
                Tags = content.Tags.Select(tag => tag.GetPresentationModel()).ToList(),
                ArchiveItems = PublicationBiz.ReadPublicationArchivedItemsCalendar(content.PublicationId.Value, ContentType.BlogPost)
                     .Select(item => new ContentArchiveItemPM(item.Item1, item.Item2)),
                Links = BlogBiz.ReadBlogLinks(content.PublicationId.Value)
                     .MapTo<BlogLinkPM>()
                     .ToList(),
                Comments = content.Comments
                    .Where(comment => !comment.IsPrivate)
                    .Select(comment => comment.GetCommentInfoPM()).ToList(),
               LatestPosts= BlogBiz.ReadUserPublishedPostsForViewByVisitor(content.Publication.CreatorId)
                    .Take(10)
                    .MapTo<ContentInfoWithTextPM>()
                    .ToList()

            };
        }

        public ViewBlogArchiveModelContainer ReadArchiveForViewByVisitor(string publicationName, int? year, int? month)
        {
            var blog = BlogBiz.Read(b => b.Name == publicationName)
                .Include(b => b.Links)
                .Single();
            var postTitles = year.HasValue && month.HasValue ?
                    BlogBiz.ReadBlogPublishedPosts(blog.Id, year.Value, month.Value) :
                    BlogBiz.ReadBlogPublishedPosts(blog.Id);
            return new ViewBlogArchiveModelContainer() {
                Year = year,
                Month = month,
                BlogInfo = blog.GetBlogInfo(),
                Links = blog.Links.Select(link => link.GetLinkPM()).ToList(),
                PostTitles = postTitles.MapTo<ContentInfo2PM>().ToList(),
                ArchiveItems = PublicationBiz.ReadPublicationArchivedItemsCalendar(blog.Id, ContentType.BlogPost)
                     .Select(item => new ContentArchiveItemPM(item.Item1, item.Item2)),
            };
        }

        public ViewBlogPostModelContainer ReadPostForPreview(string publicationName, int postId)
        {
            var content = BlogBiz.ReadPostForPreview(publicationName, postId);
            VisitBiz.IncrementContentVisits(content.Id);
            UnitOfWork.SaveChanges();
            return new ViewBlogPostModelContainer() {
                BlogInfo = BlogBiz.Read(b => b.Id == content.PublicationId).MapTo<BlogInfoPM>().Single(),
                Post = content.GetContentInfoWithTextPM(),
                Tags = content.Tags.Select(tag => tag.GetPresentationModel()).ToList(),
            };
        }

        #endregion

        #region Blog Link CRUD
        public DataSourceResult ReadBlogLinks(int blogId, DataSourceRequest request)
        {
            return FriendBiz.Read(friend => friend.BlogId == blogId)
                .MapTo<BlogLinkPM>()
                .ToDataSourceResult(request);
        }

        public void AddBlogLink(int blogId, BlogLinkPM blogFriend)
        {
            var friend = blogFriend.GetLink();
            friend.BlogId = blogId;
            FriendBiz.Add(friend);
            UnitOfWork.SaveChanges();
        }
        public void EditBlogLink(int blogId, BlogLinkPM blogFriend)
        {
            var friend = FriendBiz.ReadSingle(f => f.BlogId == blogId && f.Id == blogFriend.Id);
            friend.Name = blogFriend.Name;
            friend.Url = blogFriend.Url;
            UnitOfWork.SaveChanges();
        }

        public void DeleteBlogLinks(int blogId, IEnumerable<int> ids)
        {
            var friends = FriendBiz.Read(f => f.BlogId == blogId && ids.Contains(f.Id));
            friends.ForEach(f => FriendBiz.Remove(f));
            UnitOfWork.SaveChanges();
        }
        #endregion

        #region Blog CRUD
        public IEnumerable<BlogInfoForGenerateLinkPM> ReadBlogs(int pageIndex, int pageSize)
        {
            return BlogBiz.Read()
                .OrderBy(blog => blog.Title)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .MapTo<BlogInfoForGenerateLinkPM>()
                .ToList();
        }
        public int ReadTotallBlogsCount()
        {
            return BlogBiz.ReadTotallBlogsCount();
        }
        #endregion

        #region Post CRUD
        public void AddPost(int blogId, ContentRegistrationPM contentPresentationModel, IEnumerable<string> tags)
        {
            var content = contentPresentationModel.GetContent();
            content.Type = ContentType.BlogPost;
            content.CultureLcid = CultureInfo.GetCultureInfo("fa-IR").LCID;
            content.Text = HtmlParser.Parse(content.Text);
            content.Text = HtmlMinifier.MinifyHtml(content.Text);
            content.PublicationId = blogId;

            ContentBiz.AddContent(content, tags);
            UnitOfWork.SaveChanges();
            contentPresentationModel.Id = content.Id;
        }

        public void EditPost(ContentRegistrationPM contentPresentationModel, IEnumerable<string> tags)
        {
            var content = contentPresentationModel.GetContent();
            content.Text = HtmlParser.Parse(content.Text);
            content.Text = HtmlMinifier.MinifyHtml(content.Text);
            ContentBiz.UpdateContent(content, tags);
            UnitOfWork.SaveChanges();
        }

        public DataSourceResult ReadUserPosts(UserIdentity user, DataSourceRequest request)
        {
            return BlogBiz.ReadUserLatestPosts(user.UserId)
                .MapTo<ContentInfo5PM>()
                .ToDataSourceResult(request);
        }

        public void ReadPostForEdit(UserIdentity userIdentity, int contentId, out ContentRegistrationPM contentPresentationModel, out List<string> tags)
        {
            var content = ContentBiz.Read(c =>
                    c.AuthorId == userIdentity.UserId &&
                    c.Type == ContentType.BlogPost &&
                    c.State != ContentState.Blocked &&
                    c.Id == contentId)
            .Include(c => c.Tags)
            .Single(c => c.Id == contentId);
            contentPresentationModel = content.GetContentRegistrationPM();
            tags = new List<string>(content.Tags.Select(tag => tag.Text));
        }
        #endregion
    }
}
