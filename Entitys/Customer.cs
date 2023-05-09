using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Customer
    {
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Age { get; set; }
        [Required]
        public decimal Income { get; set; }
        public CustomerWorkingStatus WorkStatus { get; set; }
        [Required]
        public int WorkStatusId { get; set; }
        public Worker Worker { get; set; }
        public IdentityUser AppUser { get; set; }

    }
}
