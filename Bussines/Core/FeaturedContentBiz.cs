using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Bussines.Core
{
    public class FeaturedContentBiz : BizBase<Follow>
    {
        public FeaturedContentBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IQueryable<Content> ReadFeaturedArticles()
        {
            return UnitOfWork.FeaturedContentRepository
                .AsQueryable()
                .Select(featuredContent => featuredContent.Content)
                .Where(content => content.State == ContentState.Published);
        }
    }
}