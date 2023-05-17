using System.ComponentModel.DataAnnotations;
using Validators;

namespace APITrassBank
{
    public class PaymentCreateDTO
    {
        [Required]
        public string AccountId { get; set; }
        [Required]
        public string LoanId { get; set; }
        [Required]
        [GreaterThanInt(1)]
        public int NumberInstallments { get; set; }
    }
}