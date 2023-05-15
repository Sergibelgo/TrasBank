using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entitys.Entity
{
    public class Account
    {
        public Guid Id { get; set; }
        public Customer Customer { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [AllowNull]
        public decimal Interest { get; set; }
        [AllowNull]
        public DateTime SaveUntil { get; set; }
        public AccountStatus AccountStatus { get; set; }
        [Required]
        public int AccountStatusId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public AccountType AccountType { get; set; }
        [Required]
        public int AccountTypeId { get; set; }
        [Required]
        public string AccountName { get; set; }
    }
}
