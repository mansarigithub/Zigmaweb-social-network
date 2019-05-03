using System;
using ZigmaWeb.Validation.Attribute;

namespace ZigmaWeb.PresentationModel.Model
{
    public class CommentRegistrationPM
    {
        public int? Id { get; set; }

        [RequiredField]
        public int ContentId { get; set; }

        [RequiredField]
        public string Text { get; set; }

        [RequiredField]
        public bool IsPrivate { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
