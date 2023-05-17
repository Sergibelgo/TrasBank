using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entitys.Entity
{
    public class ATM
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool Operative { get; set; } = true;
        [Required]
        public IdentityUser AppUser { get; set; }
    }
}
