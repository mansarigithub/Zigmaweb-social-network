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
using ZigmaWeb.PresentationModel.Model.Archive;

namespace ZigmaWeb.Bussines.Core
{
    public class PublicationBiz : BizBase<Publication>
    {
        public PublicationBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public IEnumerable<Tuple<int, int>> ReadPublicationArchivedItemsCalendar(int publicationId, ContentType contentType)
        {
            return UnitOfWork.ContentRepository.Read(c =>
                        c.PublicationId == publicationId &&
                        c.Type == contentType &&
                        c.State == ContentState.Published)
                 .OrderBy(c => c.CreateDate)
                 .GroupBy(content => new { content.PersianCreateDateYear, content.PersianCreateDateMonth })
                 .Select(group => new { group.Key.PersianCreateDateYear, group.Key.PersianCreateDateMonth })
                 .ToList()
                 .Select(x => new Tuple<int, int>(x.PersianCreateDateYear, x.PersianCreateDateMonth));
        }
    }
}