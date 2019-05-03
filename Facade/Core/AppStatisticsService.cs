using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Common.Configuration;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.DataProvider;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.Common.Globalization;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;

namespace ZigmaWeb.Facade.Core
{
    public class AppStatisticsService
    {
        CoreUnitOfWork UnitOfWork { get; }
        ArticleBiz ArticleBiz { get; }
        BlogBiz BlogBiz { get; }
        CommentBiz CommentBiz { get; }
        VisitBiz VisitBiz { get; }
        UserBiz UserBiz { get; }
        MessageBiz MessageBiz { get; }
        FeaturedContentBiz FeaturedContentBiz { get; }

        public AppStatisticsService()
        {
            UnitOfWork = new CoreUnitOfWork();
            ArticleBiz = new ArticleBiz(UnitOfWork);
            BlogBiz = new BlogBiz(UnitOfWork);
            VisitBiz = new VisitBiz(UnitOfWork);
            UserBiz = new UserBiz(UnitOfWork);
            CommentBiz = new CommentBiz(UnitOfWork);
            MessageBiz = new MessageBiz(UnitOfWork);
            FeaturedContentBiz = new FeaturedContentBiz(UnitOfWork);
        }

        public VisitorHomePageModelContainer ReadStatisticsForHomePage()
        {
            var topVisits = VisitBiz.Read().GroupBy(r => r.ContentId)
                .Select(group => new {ContentId = group.Key, TotalVisit = group.Sum(r => r.Count)})
                .OrderByDescending(r => r.TotalVisit)
                .Take(AppConfigurationManager.TopArticlesNumber);

            var topArticlesVisits = from a in ArticleBiz.ReadPublishedArticles()
                join av in topVisits
                    on a.Id equals av.ContentId
                select new { a, av.TotalVisit};

            var topArticles = topArticlesVisits.OrderByDescending(t => t.TotalVisit).Select(t => t.a);

            return new VisitorHomePageModelContainer()
            {
                TopArticles =
                    topArticles.MapTo<ContentInfo6PM>()
                        .ToList(),
                LatestArticles =
                    ArticleBiz.ReadLatestArticles(AppConfigurationManager.LatestArticlesNumber)
                        .MapTo<ContentInfo6PM>()
                        .ToList(),
                FeaturedArticles = FeaturedContentBiz.ReadFeaturedArticles().MapTo<ContentInfo6PM>().ToList(),
            };
        }

        public AdminDashboardModelContainer GetAdminDashboardStatistics()
        {
            var today = DateTime.Now.Date;
            return new AdminDashboardModelContainer() {
                TodayVisitsCount =
                    VisitBiz.GetTodaySiteTotalVisitsCount(),
                TotalArticlesCount =
                    ArticleBiz.Read(c => c.State == ContentState.Published && c.Type == ContentType.Article).Count(),
                TotalBlogPostCount =
                    ArticleBiz.Read(c => c.State == ContentState.Published && c.Type == ContentType.BlogPost).Count(),
                TotalUsersCount = UserBiz.Read().Count(),
                NewUsers = UserBiz.GetNewUsers(top: 3).MapTo<AdminDashboardNewUserPm>().ToList(),
                NewArticles = ArticleBiz.ReadLatestArticles(3).MapTo<AdminDashboardNewArticlePm>().ToList()
            };
        }

        public IEnumerable<ChartData> GetUsersRegistrationStatisticByDate()
        {
            return UserBiz.Read(u => true, true).GroupBy(u => DbFunctions.TruncateTime(u.Membership.CreateDate))
                .Select(group => new ChartData { Date = group.Key.Value, Value = group.Count() })
                .OrderBy(u => u.Date);
        }

        public IEnumerable<ChartData> GetArticlePublishStatisticByDate()
        {
            return ArticleBiz.Read(c => c.State == ContentState.Published && c.Type == ContentType.Article, true).GroupBy(c => DbFunctions.TruncateTime(c.CreateDate))
                .Select(group => new ChartData { Date = group.Key.Value, Value = group.Count() })
                .OrderBy(a => a.Date);
        }

        public IEnumerable<ChartData> GetBlogPostPublishStatisticByDate()
        {
            return ArticleBiz.Read(c => c.State == ContentState.Published && c.Type == ContentType.BlogPost, true).GroupBy(c => DbFunctions.TruncateTime(c.CreateDate))
                .Select(group => new ChartData { Date = group.Key.Value, Value = group.Count() })
                .OrderBy(p => p.Date);
        }

        public IEnumerable<ChartData> GetSiteTotalVisistsByDate()
        {
            return VisitBiz.Read(v => true, true).GroupBy(v => DbFunctions.TruncateTime(v.Date))
                .Select(group => new ChartData { Date = group.Key.Value, Value = group.Sum(visit => visit.Count) })
                .OrderBy(v => v.Date);
        }

        public IEnumerable<ChartData> GetAuthorTotalVisitsChartData(UserIdentity user)
        {
            return VisitBiz.Read(v => v.Content.AuthorId == user.UserId, true).GroupBy(v => DbFunctions.TruncateTime(v.Date))
                .Select(group => new ChartData { Date = group.Key.Value, Value = group.Sum(visit => visit.Count) })
                .OrderBy(v => v.Date);

        }

        public IEnumerable<ChartData> GetAuthorTotalArticlesVisitsChartData(UserIdentity user)
        {
            return VisitBiz.Read(v => v.Content.AuthorId == user.UserId && v.Content.Type == ContentType.Article, true).GroupBy(v => DbFunctions.TruncateTime(v.Date))
                      .Select(group => new ChartData { Date = group.Key.Value, Value = group.Sum(visit => visit.Count) })
                      .OrderBy(v => v.Date);
        }

        public IEnumerable<ChartData> GetAuthorTotalBlogPostsChartData(UserIdentity user)
        {
            return VisitBiz.Read(v => v.Content.AuthorId == user.UserId && v.Content.Type == ContentType.BlogPost, true).GroupBy(v => DbFunctions.TruncateTime(v.Date))
              .Select(group => new ChartData { Date = group.Key.Value, Value = group.Sum(visit => visit.Count) })
              .OrderBy(v => v.Date);
        }

        public DashboardModelContainer GetUserDashboardData(int userId)
        {
            var today = DateTime.Now.Date;
            return new DashboardModelContainer() {
                PendingComments = CommentBiz.ReadNotConfirmedComments(userId).Take(4).MapTo<CommentInfoPM>().ToList(),
                ArticlesCount = ArticleBiz.ReadUserPublishedContents(userId, ContentType.Article).Count(),
                BlogPostsCount = BlogBiz.ReadUserTotalPublishedPostsCount(userId),
                TodayTotallVisits = VisitBiz.ReadUserTodayTotalVisits(userId),
                LatestMessages = MessageBiz.ReadUserMessages(userId, 4).MapTo<MessagePM>().ToList()
            };
        }

        public IEnumerable<KeyValuePair<string, int>> GetContentVisitChartDataForUserDashboard(int userId, ContentType contentType)
        {
            var chartData = VisitBiz.GetContentVisitChartDataForUserDashboard(userId, contentType);
            ChartDataHelper.FillBlankDays<ChartData>(chartData);
            return chartData.Select(data => new KeyValuePair<string, int>(data.Date.ToPersianDate("MMDD"), data.Value));
        }
    }
}
