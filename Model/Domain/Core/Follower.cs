using System;
using System.Collections.Generic;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Follow
    {
        public Follow()
        {
        }

        public int Id { get; set; }
        public int FollowerId { get; set; }
        public int FollowedId { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User Follower { get; set; }
        public virtual User Followed { get; set; }

    }
}
