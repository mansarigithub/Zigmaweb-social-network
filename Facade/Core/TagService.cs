using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System;
using System.Collections;
using ZigmaWeb.PresentationModel.Model;
using System.Collections.Generic;
using ZigmaWeb.Mapper.Core;

namespace ZigmaWeb.Facade.Core
{
    public class TagService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        TagBiz TagBiz { get; set; }
        ArticleBiz ArticleBiz { get; set; }

        public TagService()
        {
            UnitOfWork = new CoreUnitOfWork();
            TagBiz = new TagBiz(UnitOfWork);
            ArticleBiz = new ArticleBiz(UnitOfWork);
        }

        public int ReadTotallTagsCount()
        {
            return TagBiz.Read().Count();
        }

        public int ReadTagArticlesCount(int tagId)
        {
            return ArticleBiz.ReadPublishedArticles()
                .Where(article => article.Tags.Any(tag => tag.Id == tagId))
                .Count();
        }

        public IEnumerable<TagPM> ReadTags(int pageIndex, int pageSize)
        {
            return TagBiz.Read(tag => tag.Contents.Any(content => content.State == ContentState.Published), true)
                .OrderBy(tag => tag.Text)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .MapTo<TagPM>()
                .ToList();
        }

        public TagPM ReadTag(int tagId)
        {
            return TagBiz.ReadSingle(tag => tag.Id == tagId).GetPresentationModel();
        }

        public IEnumerable<ContentInfo3PM> ReadTagContents(int tagId, int pageIndex, int pageSize)
        {
            return ArticleBiz.ReadPublishedArticles()
                .OrderByDescending(content => content.Id)
                .Where(article => article.Tags.Any(tag => tag.Id == tagId))
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .MapTo<ContentInfo3PM>()
                .ToList();
        }

    }
}
