using System;
using System.Collections.Generic;

namespace ZigmaWeb.Model.Entity.Membership
{
    public partial class Role 
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
