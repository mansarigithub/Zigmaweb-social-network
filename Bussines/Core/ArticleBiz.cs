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

namespace ZigmaWeb.Bussines.Core
{
    public class ArticleBiz : ContentBiz
    {
        public ArticleBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<Content> ReadPublishedArticles()
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Type == ContentType.Article &&
                    content.State == ContentState.Published);
        }

        public IQueryable<Content> ReadPublishedContents()
        {
            return UnitOfWork.ContentRepository
                .Read(content => content.State == ContentState.Published);
        }

        public IQueryable<Content> ReadTopArticles(int count)
        {
            return ReadPublishedArticles()
                .OrderByDescending(content => content.Visits.Count)
                .Take(count);
        }

        public IQueryable<Content> ReadLatestArticles(int count)
        {
            return ReadPublishedArticles()
                .OrderByDescending(content => content.CreateDate)
                .Take(count);
        }

        public IQueryable<Content> ReadUserRelatedArticles(int userId, int contentId, int count)
        {
            var currentContentTags =
                 UnitOfWork.TagRepository.Read(t => t.Contents.Any(c => c.Id == contentId)).Select(t => t.Id);

            var userAllRelatedContents = (from c in ReadUserPublishedContents(userId, ContentType.Article).Where(content =>content.Id != contentId)
                from t in c.Tags.Where(t => currentContentTags.Contains(t.Id))
                select new {ContentId = c.Id, TagId = t.Id});

            var userTopRelatedContentIds = userAllRelatedContents.GroupBy(r => r.ContentId)
                .Select(group => new { ContentId = group.Key, RelatedTagCount = group.Count() })
                .OrderByDescending(r => r.RelatedTagCount)
                .Take(count);

            var topRelatedContents =
                UnitOfWork.ContentRepository.Read(c => userTopRelatedContentIds.Any(x => x.ContentId == c.Id));

            return topRelatedContents;
        }

        public IQueryable<Content> ReadRelatedArticles(int skipUserId,int contentId, int count)
        {
            var currentContentTags =
                UnitOfWork.TagRepository.Read(t => t.Contents.Any(c => c.Id == contentId)).Select(t => t.Id);

            var allRelatedContents = (from c in ReadPublishedArticles().Where(content => content.Id != contentId && content.AuthorId != skipUserId)
                                      from t in c.Tags.Where(t => currentContentTags.Contains(t.Id))
                select new {ContentId = c.Id, TagId = t.Id});

            var topRelatedContentIds = allRelatedContents.GroupBy(r => r.ContentId)
                .Select(group => new {ContentId = group.Key, RelatedTagCount = group.Count()})
                .OrderByDescending(r => r.RelatedTagCount)
                .Take(count);

            var topRelatedContents =
                UnitOfWork.ContentRepository.Read(c => topRelatedContentIds.Any(x => x.ContentId == c.Id));

            return topRelatedContents;
        }

        public Content ReadArticleForViewByVisitor(int contentId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Type == ContentType.Article &&
                    content.State == ContentState.Published)
                .Include(c => c.Author.ProfileKeyValues)
                .Include(c => c.Comments)
                .Include(c => c.Tags)
                .Single(content => content.Id == contentId);
        }

        public Content ReadArticleForPreview(int contentId)
        {
            return UnitOfWork.ContentRepository
                .Read(content =>
                    content.Type == ContentType.Article)
                .Include(c => c.Author.ProfileKeyValues)
                .Include(c => c.Tags)
                .Single(content => content.Id == contentId);
        }

        public int ReadTotalArticlesCount()
        {
            return UnitOfWork.ContentRepository
                .AsQueryable()
                .Count(content => content.Type == ContentType.Article);
        }

        public int ReadTotalPublishedArticlesCount()
        {
            return ReadPublishedArticles().Count();
        }

        public IQueryable<Content> ReadUserLatestPublishedArticles(int userId, int count)
        {
            return ReadUserPublishedContents(userId, ContentType.Article)
                .OrderByDescending(content => content.CreateDate)
                .Take(count);
        }
    }
}