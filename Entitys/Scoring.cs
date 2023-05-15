using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Scoring
    {
        public Guid Id { get; set; }
        public Loan Loan { get; set; }
        [Required]
        public DateTime DateTime { get; set; }=DateTime.Now;
        [Required]
        public decimal Deposit { get; set; }
        [Required]
        public decimal Spens { get; set; }
        [Required]
        public decimal Salary { get; set; }
    }
}
