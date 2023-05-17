using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Entitys.Entity
{
    public class Transaction
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Account Account { get; set; }
        public TranssactionType TransactionType { get; set; }
        [Required]
        public decimal Ammount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        [Required]
        public IdentityUser OtherInvolved { get; set; }
        [AllowNull]
        public string Concept { get; set; }
    }
}
