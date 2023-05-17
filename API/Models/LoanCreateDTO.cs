using System.ComponentModel.DataAnnotations;
using Validators;
namespace APITrassBank
{
    public class LoanCreateDTO
    {

        [Required]
        [GreaterThanDec(500.00F)]
        public float Ammount { get; set; }
        [Required]
        [GreaterThanInt(2)]
        public int TotalInstallments { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
        [Required]
        [LoanTypeValidator]
        public int TIN_TAE { get; set; }
    }
    public class LoanCreateWorkerDTO : LoanCreateDTO
    {
        [Required]
        public string CustomerId { get; set; }
        public string LoanName { get; set; }

    }
}