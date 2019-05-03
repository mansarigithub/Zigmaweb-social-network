using System;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Text { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsRead { get; set; }

        public virtual User Sender { get; set; }
        public virtual User Receiver { get; set; }
    }
}
