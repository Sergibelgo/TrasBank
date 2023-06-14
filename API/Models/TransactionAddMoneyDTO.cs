using System.ComponentModel.DataAnnotations;
using Validators;

namespace APITrassBank
{
    public class TransactionAddMoneyDTO
    {
        [Required]
        [TransactionMoneyValidator]
        public decimal Quantity { get; set; }
        public string Concept { get;set; }
    }
    public class TransferMoneyDTO
    {
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public string AccountReciverId { get; set; }
        [Required]
        public string AccountSenderId { get; set; }
        public string Concept { get; set; }
    }
}