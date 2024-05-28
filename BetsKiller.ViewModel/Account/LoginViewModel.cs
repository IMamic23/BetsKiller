using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The \"Email address\" property is required")]
        [Display(Name = "Email address")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The \"Email address\" must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The \"Password\" property is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The \"Password\" must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
