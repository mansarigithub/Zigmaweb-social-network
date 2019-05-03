using System.ComponentModel.DataAnnotations;
using ZigmaWeb.Common.Enum;
using ZigmaWeb.UI.Properties;

namespace ZigmaWeb.UI.Areas.User.ViewModels.UserManagement
{
    public class EditUserViewModel
    {
        public int UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        public Sexuality Sexuality { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        public bool IsLockedOut { get; set; }

        [Display(ResourceType = typeof(Resources), Name = "Password")]
        [MinLength(4, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MsgMinPasswordLengthIs4")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MsgMaxPasswordLengthIs50")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MsgPasswordConfirmMismatch")]
        [Display(ResourceType = typeof(Resources), Name = "NewPasswordConfirm")]
        [MinLength(4, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MsgMinPasswordLengthIs4")]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "MsgMaxPasswordLengthIs50")]
        public string NewPasswordConfirm { get; set; }

        public bool UserHasAdminRole { get; set; }

        public bool UserHasSuperUserRole { get; set; }

        public EditUserViewModel()
        {
        }

        //public EditUserViewModel(ZigmaWeb.Model.Domain.Core.User user)
        //{
        //    UserId = user.Id;
        //    Email = user.Email;
        //    FirstName = user.FirstName;
        //    LastName = user.LastName;
        //    Sexuality = user.Sexuality;
        //}

        //public void CopyToUser(ZigmaWeb.Model.Domain.Core.User target)
        //{
        //    target.Id = UserId ;
        //    target.Email = Email ;
        //    target.FirstName = FirstName;
        //    target.LastName = LastName ;
        //    target.Sexuality = Sexuality;
        //}

    }
}