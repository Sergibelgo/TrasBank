using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entitys.Entity
{
    public class Account
    {
        [Required]
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [AllowNull]
        public decimal Interest { get; set; }
        [AllowNull]
        public DateTime SaveUntil { get; set; }
        [Required]
        public AccountStatus AccountStatus { get; set; }
        [Required]
        public int AccountStatusId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public AccountType AccountType { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public string AccountName { get; set; }
    }
}
