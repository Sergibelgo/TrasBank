using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class LoanType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
