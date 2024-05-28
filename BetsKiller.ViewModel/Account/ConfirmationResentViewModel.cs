using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.Account
{
    public class ConfirmationResentViewModel
    {
        [Required(ErrorMessage = "The \"Email\" property is required")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The \"Email\" must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }
    }
}
