using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Confirm Password did not match.")]
        public string ConfirmPassword { get; set; }
    }
}
