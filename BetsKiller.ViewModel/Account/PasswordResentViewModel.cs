﻿using System.ComponentModel.DataAnnotations;

namespace BetsKiller.ViewModel.Account
{
    public class PasswordResentViewModel
    {
        [Required(ErrorMessage = "The \"Email\" property is required")]
        [EmailAddress]
        [StringLength(100, ErrorMessage = "The \"Email\" must be at least {2} characters long.", MinimumLength = 8)]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string ResetToken { get; set; }
    }
}
