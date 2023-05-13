using System.ComponentModel.DataAnnotations;

namespace APITrassBank
{
    public class LoanCreateDTO
    {
        [Required]
        public string CustomerId{ get; set; }
        [Required]
        public decimal Ammount { get; set; }
        [Required]
        public int InterestRate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        
        public DateTime EndDate { get; set; }
        [Required]
        public int TotalInstallments { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
    }
}