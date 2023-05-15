using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class LoanType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public float TIN { get; set; }
        [Required]
        public float TAE { get; set; }
        [Required]
        public float Percentaje { get; set; }
    }
}
