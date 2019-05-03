using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Common.Enum;

namespace ZigmaWeb.Model.Domain.Core
{
    public partial class User
    {
        public User()
        {
            this.Comments = new List<Comment>();
            this.Contents = new List<Content>();
            this.Roles = new List<Role>();
            this.ProfileKeyValues = new List<ProfileKeyValue>();
            this.Publications = new List<Publication>();
            this.EducationalResumes = new List<EducationalResume>();
            this.JobResumes = new List<JobResume>();
            this.MediaFiles = new List<Media>();
        }

        public int Id { get; set; }

        [StringLength(25)]
        public string FirstName { get; set; }

        [StringLength(25)]
        public string LastName { get; set; }
        public string Email { get; set; }
        public Sexuality? Sexuality { get; set; }
        public DateTime? BirthDate{ get; set; }
        public virtual Membership Membership { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<ProfileKeyValue> ProfileKeyValues { get; set; }
        public virtual ICollection<Publication> Publications { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Notification> CausedNotifications { get; set; }
        public virtual ICollection<EducationalResume> EducationalResumes { get; set; }
        public virtual ICollection<JobResume> JobResumes { get; set; }
        public virtual ICollection<Media> MediaFiles { get; set; }
        public virtual ICollection<Follow> Followers { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
