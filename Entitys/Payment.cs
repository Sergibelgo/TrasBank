using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Payment
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Loan Loan { get; set; }
        [Required]
        public decimal Ammount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
