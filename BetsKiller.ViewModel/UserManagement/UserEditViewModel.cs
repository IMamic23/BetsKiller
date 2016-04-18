using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetsKiller.ViewModel.UserManagement
{
    public class UserEditViewModel
    {
        [Required]
        [Display(Name = "Full name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Email address")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Display(Name = "Reset password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Role active from")]
        [DataType(DataType.Date)]
        public string RoleActiveFrom { get; set; }

        [Display(Name = "Role active to")]
        [DataType(DataType.Date)]
        public string RoleActiveTo { get; set; }

        [Display(Name = "Money paid")]
        [DataType(DataType.Currency)]
        public string MoneyPaid { get; set; }

        [Display(Name = "Payment description")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} can be maximum {2} characters long.")]
        public string PaymentDescription { get; set; }

        [Display(Name = "Payment sources")]
        public List<string> PaymentSources { get; set; }

        [Required]
        public List<string> Roles { get; set; }

        public List<SelectListItem> AllRoles { get; set; }

        public List<SelectListItem> AllPaymentSources { get; set; }
    }
}
