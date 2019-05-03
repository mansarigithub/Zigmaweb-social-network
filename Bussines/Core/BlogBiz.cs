using System;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.Enum;
using System.Linq;
using System.Collections.Generic;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Mapper.Core;
using System.Data.Entity;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.Model;

namespace ZigmaWeb.Bussines.Core
{
    public class BlogBiz : BizBase<Blog>
    {
        public BlogBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<Content> ReadUserPublishedPostsForViewByVisitor(int userId)
        {
            return UnitOfWork.ContentRepository
                .Include(content => content.Tags)
                .OrderByDescending(content => content.CreateDate)
                .Where(content =>
                    content.AuthorId == userId &&
                    content.Type == ContentType.BlogPost &&
                    content.State == ContentState.Published);
        }

        public IQueryable<Content> ReadUserLatestPosts(int userId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.AuthorId == userId &&
                    content.Type == ContentType.BlogPost &&
                    content.State != ContentState.Trashed)
                .OrderByDescending(content => content.CreateDate);
        }

        public IQueryable<Content> ReadUserLatestPosts(int userId, int count)
        {
            return ReadUserPublishedPostsForViewByVisitor(userId).Take(count);
        }

        public IQueryable<Content> ReadTopPosts(int count)
        {
            return UnitOfWork.ContentRepository
                .Read(content => content.Type == ContentType.BlogPost)
                .OrderBy(content => content.Visits.Count)
                .Take(count);
        }

        public int ReadTotalPostsCount()
        {
            return UnitOfWork.ContentRepository
                .AsQueryable()
                .Count(content => content.Type == ContentType.BlogPost);
        }

        public IQueryable<Model.Domain.Core.BlogLink> ReadBlogLinks(int blogId)
        {
            return UnitOfWork.FriendRepository.Read(f => f.BlogId == blogId);
        }

        public void CreateBlog(Blog blog)
        {
            blog.Name = blog.Name.Trim().ToLower();
            blog.CreateDate = DateTime.Now;
            if (Any(b => b.Name == blog.Name))
                throw new BusinessException("BlogNameIsNotAvailable".Localize());
            Add(blog);
        }

        public Content ReadPostForViewByVisitor(string publicationName, int contentId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Publication.Name == publicationName &&
                    content.Type == ContentType.BlogPost &&
                    content.State == ContentState.Published)
                .Include(c => c.Author)
                .Include(c => c.Tags)
                .Include(c=>c.Publication)
                .Include(c => c.Comments.Select(comment => comment.Sender))
                .Single(content => content.Id == contentId);
        }

        public IQueryable<Content> ReadBlogPublishedPosts(int blogId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Publication.Id == blogId &&
                    content.Type == ContentType.BlogPost &&
                    content.State == ContentState.Published);
        }

        public IQueryable<Content> ReadBlogPublishedPosts(int blogId, int persianCreateYear, int persianCreateMonth)
        {
            return ReadBlogPublishedPosts(blogId)
                .Where(c => c.PersianCreateDateYear == persianCreateYear && c.PersianCreateDateMonth == persianCreateMonth);
        }

        public IQueryable<Content> ReadBlogPublishedPosts(string blogName)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Publication.Name == blogName &&
                    content.Type == ContentType.BlogPost &&
                    content.State == ContentState.Published);
        }

        public Content ReadPostForPreview(string publicationName, int contentId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Publication.Name == publicationName &&
                    content.Type == ContentType.BlogPost)
                .Include(c => c.Author)
                .Include(c => c.Tags)
                .Single(content => content.Id == contentId);
        }

        public int ReadTotallBlogsCount()
        {
            return UnitOfWork.BlogRepository.AsQueryable().Count();
        }

        public int ReadUserTotalPublishedPostsCount(int userId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.AuthorId == userId &&
                    content.Type == ContentType.BlogPost &&
                    content.State == ContentState.Published).Count();
        }
    }
}