using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Bussines.Core
{
    public class MediaBiz : BizBase<Media>
    {
        public MediaBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public Media AddMedia(string fileName, int size, MediaType type, int userId)
        {
            return Add(new Media() {
                FileName = fileName,
                Size = size,
                Type = type,
                UserId = userId,
                CreateDate  = DateTime.Now,
            });
        }

        public void Remove(int id, int userId)
        {
            Remove(ReadSingle(m => m.Id == id && m.UserId == userId));
        }
    }
}