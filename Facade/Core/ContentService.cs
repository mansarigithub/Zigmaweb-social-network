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

namespace ZigmaWeb.Facade.Core
{
    public class ContentService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        ContentBiz ContentBiz { get; set; }
        TagBiz TagBiz { get; set; }

        public ContentService()
        {
            UnitOfWork = new CoreUnitOfWork();
            ContentBiz = new ContentBiz(UnitOfWork);
            TagBiz = new TagBiz(UnitOfWork);
        }

        public DataSourceResult GetAllContents(DataSourceRequest request)
        {
            return ContentBiz.Read(content =>
                content.State != ContentState.Trashed, noTracking: true).Include(c => c.Author)
                .MapTo<ContentInfo1PM>()
                .ToDataSourceResult(request);
        }

        public void ChangeContentsState(IEnumerable<int> ids, ContentState state, UserIdentity userIdentity)
        {
            ContentBiz.Read(content =>
                    content.AuthorId == userIdentity.UserId &&
                    ids.Contains(content.Id))
                .ToList()
                .ForEach(content => content.State = state);
            UnitOfWork.SaveChanges();
        }

        public void ChangeContentsState(IEnumerable<int> ids, ContentState state)
        {
            ContentBiz.Read(content =>
                    ids.Contains(content.Id))
                .ToList()
                .ForEach(content => content.State = state);
            UnitOfWork.SaveChanges();
        }

        public void RemoveContents(IEnumerable<int> ids, UserIdentity userIdentity)
        {
            var contentsToRemove = ContentBiz.Read(a => ids.Contains(a.Id) && a.AuthorId == userIdentity.UserId);
            HashSet<string> pictureNamesToRemove = new HashSet<string>();
            foreach (var content in contentsToRemove)
            {
                ContentBiz.Remove(content);
            }
            UnitOfWork.SaveChanges();
        }
    }
}
