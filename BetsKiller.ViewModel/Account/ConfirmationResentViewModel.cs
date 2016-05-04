using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
