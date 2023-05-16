using System.ComponentModel.DataAnnotations;
using Validators;

namespace APITrassBank
{
    public class TransactionAddMoneyDTO
    {
        [Required]
        [TransactionMoneyValidator]
        public decimal Quantity { get; set; }
    }
    public class TransferMoneyDTO 
    {
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public string accountReciverId { get; set; }
        [Required]
        public string accountSenderId { get; set; }
    }
}