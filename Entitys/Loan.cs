using System.ComponentModel.DataAnnotations;
using Validators;

namespace Entitys.Entity
{
    public class Loan
    {
        public Guid Id { get; set; }
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
        public LoanType LoanType { get; set; }
        [Required]
        public int LoanTypeId { get; set; }
    }
}
