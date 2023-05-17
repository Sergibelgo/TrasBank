using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
