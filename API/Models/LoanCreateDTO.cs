using System.ComponentModel.DataAnnotations;
using Validators;
namespace APITrassBank
{
    public class LoanCreateDTO
    {
        [Required]
        public string CustomerId{ get; set; }
        [Required]
        [GreaterThanInt(500)]
        public decimal Ammount { get; set; }
        [Required]
        [GreaterThanInt(1)]
        public int InterestRate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [GreaterThan("StartDate")]
        public DateTime EndDate { get; set; }
        [Required]
        [GreaterThanInt(2)]
        public int TotalInstallments { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
    }
}