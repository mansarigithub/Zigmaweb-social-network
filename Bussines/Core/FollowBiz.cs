using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using System;
using System.Data.Entity;
using System.Linq;

namespace ZigmaWeb.Bussines.Core
{
    public class FollowBiz : BizBase<Follow>
    {
        public FollowBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {
        }

        public bool AnyFollow(int followerId, int followedId)
        {
            return Any(f => f.FollowerId == followerId && f.FollowedId == followedId);
        }

        public int ReadFollowersCount(int userId)
        {
            return Read(f => f.FollowedId == userId).Count();
        }
    }
}