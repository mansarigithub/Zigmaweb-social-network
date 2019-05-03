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
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.PresentationModel.Model;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.PresentationModel.ModelContainer;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using Common.Exception;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.PresentationModel.ModelContainer.Blog;
using ZigmaWeb.PresentationModel.Model.Archive;

namespace ZigmaWeb.Facade.Core
{
    public class PublicationService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        BlogBiz BlogBiz { get; set; }
        PublicationBiz PublicationBiz { get; set; }
        ContentBiz ContentBiz { get; set; }

        public PublicationService()
        {
            UnitOfWork = new CoreUnitOfWork();
            BlogBiz = new BlogBiz(UnitOfWork);
            PublicationBiz = new PublicationBiz(UnitOfWork);
            ContentBiz = new ContentBiz(UnitOfWork);
        }

        public object ReadPublicationHomePageData(string publicationName)
        {
            var publication = PublicationBiz.Include(p => p.Creator).SingleOrDefault(p => p.Name == publicationName);
            if (publication == null)
                throw new PageNotFoundException();
            if (publication is Blog)
                return ReadBlogHomePageData((Blog)publication);
            else
                return ReadChannelHomePageData((Channel)publication);
        }

        private BlogHomePageModelContainer ReadBlogHomePageData(Blog blog)
        {
            return new BlogHomePageModelContainer() {
                BlogInfo = blog.GetBlogInfo(),
                ArchiveItems = PublicationBiz.ReadPublicationArchivedItemsCalendar(blog.Id, ContentType.BlogPost)
                     .Select(item => new ContentArchiveItemPM(item.Item1, item.Item2)),
                Links = BlogBiz.ReadBlogLinks(blog.Id)
                     .MapTo<BlogLinkPM>()
                     .ToList(),
                LatestPosts = BlogBiz.ReadUserPublishedPostsForViewByVisitor(blog.CreatorId)
                    .Take(100)
                    .MapTo<ContentInfoWithTextPM>()
                    .ToList()
            };
        }

        private object ReadChannelHomePageData(Channel hannel)
        {
            throw new NotImplementedException();
        }
    }
}
