using ZigmaWeb.Bussines.Core;
using ZigmaWeb.Security.Helper;
using System.Linq;
using System.Data.Entity;
using ZigmaWeb.Security.Identity;
using ZigmaWeb.UnitOfWork;
using ZigmaWeb.PresentationModel.Model;
using System;
using ZigmaWeb.Mapper.Core;
using ZigmaWeb.Model.Domain.Core;
using ZigmaWeb.Common.Messaging;
using ZigmaWeb.Localization.ExtensionMethod;
using System.Text;
using Common.Exception;
using ZigmaWeb.Security.ExtensionMethod;
using ZigmaWeb.Common.Configuration;

namespace ZigmaWeb.Facade.Core
{
    public class UserRegistrationService
    {
        CoreUnitOfWork UnitOfWork { get; set; }
        UserBiz UserBiz { get; set; }
        MembershipBiz MembershipBiz { get; set; }
        ExceptionLogBiz Logger { get; set; }
        public UserRegistrationService()
        {
            UnitOfWork = new CoreUnitOfWork();
            UserBiz = new UserBiz(UnitOfWork);
            MembershipBiz = new MembershipBiz(UnitOfWork);
            Logger = new ExceptionLogBiz(UnitOfWork);
        }

        #region User Registration
        public void RegisterUser(UserRegistrationPM userRegistrationPM, bool sendConfirmationEmail)
        {
            var user = UserBiz.CreateUser(userRegistrationPM.GetUser());
            var membership = MembershipBiz.CreateMembershipForNewUser(user, userRegistrationPM.Password);

            if (sendConfirmationEmail)
            {
                try
                {
                    SendActivationEmail(user, membership);
                }
                catch(Exception ex)
                {
                    Logger.AddException(ex, "signIn");
                }
            }
            UnitOfWork.SaveChanges();
        }

        #endregion

        #region User Account Activation
        public void ResendActivationEmail(string email)
        {
            email = email.Trim().ToLower();
            var user = UserBiz.Include(u => u.Membership).SingleOrDefault(u => u.Email == email && u.Membership.IsApproved == false);
            if (user == null)
                return;
            user.Membership.VerificationCode = Guid.NewGuid();
            user.Membership.VerificationCodeSendDate = DateTime.Now;
            UnitOfWork.SaveChanges();
            SendActivationEmail(user, user.Membership);
        }

        public void ActivateAccount(int userId, Guid verificationCode)
        {
            var membership = MembershipBiz.ReadSingleOrDefault(m => m.Id == userId && m.VerificationCode == verificationCode);
            if (membership == null || membership.IsApproved)
                throw new BusinessException("InvalidAccountActivationInfo".Localize());
            if (membership.VerificationCodeSendDate < DateTime.Now.AddDays(-7))
                throw new BusinessException("AccountActivationLinkHasExpired".Localize());
            membership.IsApproved = true;
            UnitOfWork.SaveChanges();
        }

        private void SendActivationEmail(User user, Membership membership)
        {
            var emailSubject = "UserAccountActivation".Localize();
            var emailBody = new StringBuilder(Common.Properties.Resources.UserRegistrationVerificationEmailTemplate);
            emailBody.Replace("{UserFullName}", user.FullName);
            emailBody.Replace("{UserId}", membership.Id.ToString());
            emailBody.Replace("{VerificationCode}", membership.VerificationCode.ToString());
            EmailHelper.SendMail("reg@zigmaweb.com", user.Email, emailSubject, emailBody.ToString(), user.FullName);
        }

        #endregion

        #region Password Reset
        public bool IsPasswordResetTokenValid(int userId, Guid passwordResetToken)
        {
            return MembershipBiz.Any(membership => membership.Id == userId && membership.PasswordResetToken == passwordResetToken);
        }

        public void ResetPassword(int userId, Guid passwordResetToken, string newPassword)
        {
            var membership = MembershipBiz.ReadSingleOrDefault(m => m.Id == userId && m.PasswordResetToken == passwordResetToken);
            if (membership == null)
                throw new BusinessException("InvalidPasswordLink");

            membership.Password = newPassword.ComputeSha256Hash();
            membership.LastPasswordChangedDate = DateTime.Now;
            membership.PasswordResetToken = null; // Invalidate Password Reset Token
            UnitOfWork.SaveChanges();
        }

        public void SendPasswordResetEmail(string email)
        {
            email = email.Trim().ToLower();
            var membership = MembershipBiz.Include(u => u.User).SingleOrDefault(m =>
                m.User.Email == email &&
                //m.IsApproved &&
                !m.IsLockedOut);
            if (membership == null)
                return;

            membership.PasswordResetToken = Guid.NewGuid();
            UnitOfWork.SaveChanges();
            SendPasswordResetEmail(membership.User, membership);
        }

        private void SendPasswordResetEmail(User user, Membership membership)
        {
            try {
                var emailSubject = "PasswordChange".Localize();
                var emailBody = new StringBuilder(Common.Properties.Resources.PasswordResetEmailTemplate);
                //emailBody.Replace("{Domain}", "http://localhost:39056");
                emailBody.Replace("{Domain}", "http://zigmaweb.com");
                emailBody.Replace("{UserId}", membership.Id.ToString());
                emailBody.Replace("{UserFullName}", user.FullName);
                emailBody.Replace("{PasswordResetToken}", membership.PasswordResetToken.Value.ToString());
                EmailHelper.SendMail("reg@zigmaweb.com", user.Email, emailSubject, emailBody.ToString(), user.FullName);
            }
            catch (Exception exp) {
                // Log it.
            }
        }
        #endregion
    }
}
