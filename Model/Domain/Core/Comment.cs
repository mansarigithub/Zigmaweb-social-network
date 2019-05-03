using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int SenderId { get; set; }
        public string Text { get; set; }
        public System.DateTime CreateDate { get; set; }
        public bool IsPrivate { get; set; }
        public CommentStatus Status { get; set; }
        public int LikeCount { get; set; }
        public int DislikeCount { get; set; }

        public virtual Content Content { get; set; }
        public virtual User Sender { get; set; }
    }
}
