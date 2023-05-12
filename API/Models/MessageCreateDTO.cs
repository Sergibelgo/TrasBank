﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class MessageCreateDTO
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public string ReciverId { get; set; }
    }
}
