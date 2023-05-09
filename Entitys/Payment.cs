using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Payment
    {
        public Guid Id { get; set; }
        public Loan Loan { get; set; }
        
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Ammount { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
