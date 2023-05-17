using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Worker
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public IdentityUser AppUser { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public WorkerStatus WorkerStatus { get; set; }
    }
}
