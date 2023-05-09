using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Account Account { get; set; }
        public TranssactionType TransactionType { get; set; }
        [Required]
        public decimal Ammount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
        public Account Sender_Account { get; set; }
    }
}
