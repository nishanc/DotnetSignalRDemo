﻿using System.ComponentModel.DataAnnotations;

namespace ServerApp.Data.DTOs
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }

        [Required]
        public int Group { get; set; }
    }
}
