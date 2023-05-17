using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APITrassBank
{
    public class ScoringCreateDTO : LoanCreateDTO
    {
        [AllowNull]
        public float Deposit { get; set; }
        [Required]
        public IEnumerable<Expenses> Expenses { get; set; }
        public string Name { get; set; }
    }
    public class Expenses
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public float Spend { get; set; }
    }
}