using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using ZigmaWeb.PresentationModel.Model.Blog;

namespace ZigmaWeb.Security.Identity
{
    public class UserIdentity : IPrincipal
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public List<ShortBlogInfoPM> Blogs { get; set; }
        public bool HasBlog { get; set; }
        public bool IsApproved { get; set; }
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public UserIdentity()
        {
            
        }

        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            return Roles.Contains(role);
        }
    } 
}