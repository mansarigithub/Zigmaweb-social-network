using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Membership
    {
        public int Id { get; set; }
        public byte[] Password { get; set; }
        public bool IsApproved { get; set; }
        public bool IsLockedOut { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> LastLoginDate { get; set; }
        public Nullable<System.DateTime> LastPasswordChangedDate { get; set; }
        public Nullable<System.DateTime> LastLockoutDate { get; set; }
        public int FailedPasswordAttemptCount { get; set; }
        public System.Guid VerificationCode { get; set; }
        public System.DateTime VerificationCodeSendDate { get; set; }
        public Guid? PasswordResetToken { get; set; }

        public virtual User User { get; set; }
    }
}
