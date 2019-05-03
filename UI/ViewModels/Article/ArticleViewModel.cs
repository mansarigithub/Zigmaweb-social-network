using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Business;
using ZigmaWeb.PresentationModel.ModelContainer;

namespace ZigmaWeb.UI.ViewModels.Article
{
    public class ArticleViewModel
    {
        public ArticleViewModel(ViewArticleModelContainer modelContainer, bool previewMode)
        {
            Article = modelContainer.Article;
            AuthorProfile = modelContainer.AuthorProfile;
            Tags = modelContainer.Tags;
            Comments = modelContainer.Comments ?? new List<CommentInfoPM>();
            RelatedArticles = modelContainer.RelatedArticles ?? new List<ContentInfo4PM>();
            UserRelatedArticles = modelContainer.UserRelatedArticles ?? new List<ContentInfo4PM>();
            TopArticles = modelContainer.TopArticles ?? new List<ContentInfo4PM>();
            PreviewMode = previewMode;
            AuthorBusinessIntroduce = modelContainer.AuthorBusinessIntroduce;
            TotalVisits = modelContainer.TotalVisits;
        }

        public ContentForViewByVisitorPM Article { get; set; }
        public ProfileForViewByVisitorPM AuthorProfile { get; set; }
        public IEnumerable<TagPM> Tags { get; set; }
        public IEnumerable<CommentInfoPM> Comments { get; set; }
        public List<ContentInfo4PM> UserRelatedArticles { get; private set; }
        public List<ContentInfo4PM> RelatedArticles { get; private set; }
        public List<ContentInfo4PM> TopArticles { get; private set; }
        public BusinessIntroducePM AuthorBusinessIntroduce { get; set; }
        public int TotalVisits { get; set; }
        public bool PreviewMode { get; set; }
    }
}