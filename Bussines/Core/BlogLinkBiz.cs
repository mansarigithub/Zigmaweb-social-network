using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;

namespace ZigmaWeb.Bussines.Core
{
    public class BlogLinkBiz : BizBase<BlogLink>
    {
        public BlogLinkBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }
    }
}