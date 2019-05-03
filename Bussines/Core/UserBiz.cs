using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Common.ExtensionMethod;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Bussines.Core
{
    public class UserBiz : BizBase<User>
    {
        public UserBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public User CreateUser(User user)
        {
            var email = user.Email.Trim().ToLower();
            if (Any(u => u.Email == email))
                throw new BusinessException("MsgEmailAddressIsAlreadyRegistered".Localize());

            return UnitOfWork.UserRepository.Add(user);
        }

        public IQueryable<User> GetNewUsers(int top)
        {
            return UnitOfWork.UserRepository.AsQueryable()
                .OrderByDescending(user => user.Membership.CreateDate)
                .Take(top);
        }

    }
}