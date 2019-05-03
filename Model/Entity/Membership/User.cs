using System;
using System.Collections.Generic;
using ZigmaWeb.Model.Entity.ZwCore;
using ZigmaWeb.Model.Enum;

namespace ZigmaWeb.Model.Entity.Membership
{
    public partial class User
    {
        public User()
        {
            this.BinaryProfiles = new List<BinaryProfile>();
            this.TextProfiles = new List<TextProfile>();
            this.Contents = new List<Content>();
            this.Roles = new List<Role>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LoweredEmail { get; set; }
        public Sexuality Sexuality { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public virtual ICollection<BinaryProfile> BinaryProfiles { get; set; }
        public virtual Membership Membership { get; set; }
        public virtual ICollection<TextProfile> TextProfiles { get; set; }
        public virtual ICollection<Content> Contents { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

    }
}
