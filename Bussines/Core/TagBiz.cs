using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;

namespace ZigmaWeb.Bussines.Core
{
    public class TagBiz : BizBase<Tag>
    {
        public TagBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }
    }
}