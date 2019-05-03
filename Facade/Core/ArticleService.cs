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
using ZigmaWeb.PresentationModel.Model.Business;

namespace ZigmaWeb.Facade.Core
{
    public class ArticleService
    {
        CoreUnitOfWork UnitOfWork { get; }
        ArticleBiz ArticleBiz { get; }
        VisitBiz VisitBiz { get; }
        TagBiz TagBiz { get; }
        CommentBiz CommentBiz { get; set; }
        public ArticleService()
        {
            UnitOfWork = new CoreUnitOfWork();
            ArticleBiz = new ArticleBiz(UnitOfWork);
            VisitBiz = new VisitBiz(UnitOfWork);
            CommentBiz = new CommentBiz(UnitOfWork);
            TagBiz = new TagBiz(UnitOfWork);
        }

        public IEnumerable<ContentInfo3PM> SearchArticles(string tagName)
        {
            var keywords =
                tagName.Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries)
                    .Select(i => i.Trim())
                    .Where(i => i.Length > 2).ToList();

            var tags = from tag in TagBiz.Read()
                from k in keywords
                where tag.Text.Contains(k)
                select tag.Id;
           
            var articles = from a in ArticleBiz.ReadPublishedArticles()
                           from k in keywords
                           where a.Title.Contains(k)
                           select a.Id;

            return ArticleBiz.ReadPublishedArticles()
                .Where(art => 
                    art.Tags.Any(tag => tags.Contains(tag.Id)) ||
                    articles.Contains(art.Id))
                .OrderByDescending(a => a.PublishDate)
                .MapTo<ContentInfo3PM>()
                .ToList();
        }

        public void AddArticle(ContentRegistrationPM contentPresentationModel, IEnumerable<string> tags)
        {
            var content = contentPresentationModel.GetContent();
            content.Type = ContentType.Article;
            content.CultureLcid = CultureInfo.GetCultureInfo("fa-IR").LCID;
            content.Text = HtmlParser.Parse(content.Text);
            content.Text = HtmlMinifier.MinifyHtml(content.Text);

            ArticleBiz.AddContent(content, tags);
            UnitOfWork.SaveChanges();
            contentPresentationModel.Id = content.Id;
        }

        public void EditArticle(ContentRegistrationPM contentPresentationModel, IEnumerable<string> tags)
        {
            var content = contentPresentationModel.GetContent();
            content.Text = HtmlParser.Parse(content.Text);
            content.Text = HtmlMinifier.MinifyHtml(content.Text);

            ArticleBiz.UpdateContent(content, tags);
            UnitOfWork.SaveChanges();
        }

        public DataSourceResult ReadUserArticles(UserIdentity user, DataSourceRequest request)
        {
            return ArticleBiz.ReadUserContents(user.UserId, ContentType.Article)
                .OrderByDescending(a => a.PublishDate)
                .MapTo<ContentInfo5PM>()
                .ToDataSourceResult(request);
        }

        public void ReadArticleForEdit(UserIdentity userIdentity, int contentId, out ContentRegistrationPM contentPresentationModel, out List<string> tags)
        {
            var content = ArticleBiz.Read(c =>
                        c.AuthorId == userIdentity.UserId &&
                        c.Type == ContentType.Article &&
                        c.State != ContentState.Blocked &&
                        c.Id == contentId)
                .Include(c => c.Tags)
                .Single();
            contentPresentationModel = content.GetContentRegistrationPM();
            tags = new List<string>(content.Tags.Select(tag => tag.Text));
        }

        public ViewArticleModelContainer ReadArticleForViewByVisitor(int contentId)
        {
            var content = ArticleBiz.ReadArticleForViewByVisitor(contentId);
            VisitBiz.IncrementContentVisits(contentId);
            UnitOfWork.SaveChanges();
            int totalVisits = 0;
            try
            {
                totalVisits = VisitBiz.Read(e => e.ContentId == contentId).Sum(e => e.Count);
            }
            catch (InvalidOperationException ex)
            {
            }

            var result = new ViewArticleModelContainer()
            {
                Article = content.GetContentForViewByVisitorPM(),
                Tags = content.Tags.Select(tag => tag.GetPresentationModel()).ToList(),
                AuthorProfile = new ProfileForViewByVisitorPM()
                {
                    AboutMe = content.Author.ProfileKeyValues.SingleOrDefault(profileKeyValye =>
                                profileKeyValye.Type == ProfileKeyValueType.AboutMe)?.Value
                },
                AuthorBusinessIntroduce = new BusinessIntroducePM()
                {
                    Text = content.Author.ProfileKeyValues.SingleOrDefault(profileKeyValye =>
                                profileKeyValye.Type == ProfileKeyValueType.UserBusinessIntroduceText)?.Value
                },
                Comments = CommentBiz.GetArticleComments(content.Id).MapTo<CommentInfoPM>().ToList(),
                UserRelatedArticles = ArticleBiz.ReadUserRelatedArticles(content.AuthorId, contentId, 10)
                    .MapTo<ContentInfo4PM>()
                    .ToList(),
                RelatedArticles = ArticleBiz.ReadRelatedArticles(content.AuthorId, contentId, 10)
                    .MapTo<ContentInfo4PM>()
                    .ToList(),
                TotalVisits = VisitBiz.Read(e => e.ContentId == contentId).Sum(e => e.Count)
            };

            if (result.UserRelatedArticles.Count == 0 && result.RelatedArticles.Count == 0)
            {
                result.TopArticles = ArticleBiz.ReadTopArticles(10)
                    .MapTo<ContentInfo4PM>()
                    .ToList();
            }
            return result;
        }

        public ViewArticleModelContainer ReadArticleForPreview(int contentId)
        {
            var content = ArticleBiz.ReadArticleForPreview(contentId);

            return new ViewArticleModelContainer()
            {
                Article = content.GetContentForViewByVisitorPM(),
                Tags = content.Tags.Select(tag => tag.GetPresentationModel()).ToList(),
                AuthorProfile = new ProfileForViewByVisitorPM()
                {
                    AboutMe = content.Author.ProfileKeyValues.SingleOrDefault(profileKeyValye =>
                                profileKeyValye.Type == ProfileKeyValueType.AboutMe)?.Value
                },
            };
        }

        public int ReadTotallPublishedArticlesCount()
        {
            return ArticleBiz.ReadTotalPublishedArticlesCount();
        }

        public IEnumerable<ContentInfo3PM> ReadArticles(int pageIndex, int pageSize)
        {
            return ArticleBiz.ReadPublishedArticles()
                .OrderBy(blog => blog.Title)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .MapTo<ContentInfo3PM>()
                .ToList();
        }
    }
}
