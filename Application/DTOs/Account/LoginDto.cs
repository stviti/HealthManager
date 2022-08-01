﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Account
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
