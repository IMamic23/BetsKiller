using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage="The \"Full name\" property is required")]
        [StringLength(50, ErrorMessage = "The \"Full name\" property must be at least {2} characters long.", MinimumLength = 8)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "The \"Email\" property is required")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The \"Email\" must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The \"Password\" property is required")]
        [DataType(DataType.Password)]
        [StringLength(30, ErrorMessage = "The \"Password\" must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The \"Confirm password\" property is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "The \"I'm down with the terms\" property is required.")]
        [Range(typeof(bool), "true", "true", ErrorMessage="You must agree to the terms and conditions.")]
        public bool TermsConfirmed { get; set; }
    }
}
