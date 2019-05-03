using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Notification
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }

        /// <summary>
        /// فاعل - کسی که باعث به وجود آمدن این نوتیفیکیشن شده
        /// </summary>
        public int? SubjectId { get; set; }

        /// <summary>
        /// مفعول - این اطلاعیه برای کدام موجودیت تولید شده
        /// </summary>
        public int? ObjectId { get; set; }

        /// <summary>
        /// گیرنده اطلاعیه
        /// </summary>
        public int ReceiverId { get; set; }

        public virtual User Subject { get; set; }
        public virtual User Receiver { get; set; }

        #region Not Mapped Properties
        [NotMapped]
        public string Message { get; set; }

        [NotMapped]
        public string Title { get; set; }

        [NotMapped]
        public string Url { get; set; }

        [NotMapped]
        public string ImageUrl { get; set; }
        #endregion
    }
}
