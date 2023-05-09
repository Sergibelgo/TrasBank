using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Worker
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IdentityUser AppUser { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public WorkerStatus WorkerStatus { get; set; }
    }
}
