using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Account
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public decimal Interest { get; set; }
        public AccountStatus AccountStatus { get; set; }
        [Required]
        public int AccountStatusId { get; set; }
    }
}
