using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace BetsKiller.ViewModel.UserManagement
{
    public class UserAddViewModel
    {
        [Required]
        [Display(Name = "Full name")]
        [StringLength(50, ErrorMessage = "The property \"Full name\" must be at least {2} characters long.", MinimumLength = 4)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The \"Email address\" must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The \"Password\" must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role active from")]
        [DataType(DataType.Date)]
        public string RoleActiveFrom { get; set; }

        [Display(Name = "Role active to")]
        [DataType(DataType.Date)]
        public string RoleActiveTo { get; set; }

        [Required]
        public List<string> Roles { get; set; }

        public List<SelectListItem> AllRoles { get; set; }
    }
}
