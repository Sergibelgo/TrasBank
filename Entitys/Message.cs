using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Message
    {
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Body { get; set; }
        public IdentityUser User { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public IdentityUser Reciver { get; set; }

        public bool IsReaded { get; set; } = false;
    }
}
