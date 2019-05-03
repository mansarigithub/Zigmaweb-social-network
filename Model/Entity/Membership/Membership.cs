using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Entity.Membership
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
        public Nullable<int> FailedPasswordAttemptCount { get; set; }
        public System.Guid VerificationCode { get; set; }
        public System.DateTime VerificationCodeSendDate { get; set; }
        public virtual User User { get; set; }
    }
}
