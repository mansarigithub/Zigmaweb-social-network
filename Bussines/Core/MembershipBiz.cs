using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Bussines.Infrastructure;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.Security.ExtensionMethod;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;

namespace ZigmaWeb.Bussines.Core
{
    public class MembershipBiz : BizBase<Membership>
    {
        public MembershipBiz(ICoreUnitOfWork coreUnitOfWork) : base(coreUnitOfWork)
        {

        }

        public Membership CreateMembershipForNewUser(User newUser, string password)
        {
            return UnitOfWork.MembershipRepository.Add(new Membership() {
                User = newUser,
                CreateDate = DateTime.Now,
                FailedPasswordAttemptCount = 0,
                IsApproved = false,
                IsLockedOut = false,
                Password = password.ComputeSha256Hash(),
                VerificationCode = Guid.NewGuid(),
                VerificationCodeSendDate = DateTime.Now,
            });
        }

        public void UpdateUserPassword(int userId, string currentPassword, string newPassword)
        {
            var membership = ReadSingle(m => m.Id == userId);
            if(!membership.Password.SequenceEqual(currentPassword.ComputeSha256Hash())) {
                throw new BusinessException("InvalidCurrentPassword".Localize());
            }
            membership.Password = newPassword.ComputeSha256Hash();
        }
    }
}