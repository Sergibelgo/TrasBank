using System.ComponentModel.DataAnnotations;
using Validators;

namespace APITrassBank
{
    public class TransactionAddMoneyDTO
    {
        [Required]
        [GreaterThanInt(5)]
        [TransactionMoneyValidator]
        public int Quantity { get; set; }
    }
}