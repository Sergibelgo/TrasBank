using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class AccountStatus
    {
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
