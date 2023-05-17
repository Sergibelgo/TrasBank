using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Loan
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public decimal Ammount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }
        [Required]
        public decimal TotalAmmount { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public int TotalInstallments { get; set; }
        [Required]
        public int RemainingInstallments { get; set; }
        [Required]
        public decimal RemainingAmmount { get; set; }
        public LoanStatus LoanStatus { get; set; }
        [Required]
        public int LoanStatusId { get; set; }
        [Required]
        public LoanType LoanType { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
        [Required]
        public string LoanName { get; set; }
    }
}
