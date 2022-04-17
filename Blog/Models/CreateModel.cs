﻿using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class CreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
