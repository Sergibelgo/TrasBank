using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class AccountStatus
    {
        [Required]  
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
