using System;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.DataAccess.Context;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Security.Helper;

namespace ZigmaWeb.DataAccess.Seed
{
    public static class DatabaseInitializer_Level1
    {
        public static void Initialize()
        {
            var context = new ZigmaWebContext();

            #region Roles
            var role1 = new Role()
            {
                Name = "admin",
                Title = "مدیر ارشد"
            };
            var role2 = new Role()
            {
                Name = "moderator",
                Title = "مدیر"
            };
            context.Roles.AddRange(new Role[] { role1, role2 });
            #endregion

            #region Users
            var user1 = new User()
            {
                FirstName = "مهرداد",
                LastName = "تاجدینی",
                Email = "mehrta@live.com",
                Sexuality = Sexuality.Male,
                Membership = new Membership()
                {
                    CreateDate = DateTime.Now,
                    FailedPasswordAttemptCount = 0,
                    IsApproved = true,
                    IsLockedOut = false,
                    Password = HashHelper.ComputeSha256Hash("12345"),
                    VerificationCode = Guid.NewGuid(),
                    VerificationCodeSendDate = DateTime.Now
                },
            };
            user1.Roles.Add(role1);
            user1.Roles.Add(role2);
            user1.ProfileKeyValues.Add(new ProfileKeyValue() { Type = ProfileKeyValueType.AboutMe, Value = "This is me." });

            var user2 = new User()
            {
                FirstName = "محمد",
                LastName = "انصاری",
                Email = "ansari.va@hotmail.com",
                Sexuality = Sexuality.Male,
                Membership = new Membership()
                {
                    CreateDate = DateTime.Now,
                    FailedPasswordAttemptCount = 0,
                    IsApproved = true,
                    IsLockedOut = false,
                    Password = HashHelper.ComputeSha256Hash("12345"),
                    VerificationCode = Guid.NewGuid(),
                    VerificationCodeSendDate = DateTime.Now,
                },
            };
            user2.Roles.Add(role1);
            user2.Roles.Add(role2);
            user2.ProfileKeyValues.Add(new ProfileKeyValue() { Type = ProfileKeyValueType.AboutMe, Value = "This is me." });

            context.Users.AddRange(new User[] { user1, user2 });
            #endregion

            context.SaveChanges();
        }
    }
}
