using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class Scoring
    {
        public Guid Id { get; set; }
        public Loan Loan { get; set; }

        [Required]
        public decimal FIN { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [Required]
        public decimal Deposit { get; set; }

    }
}
