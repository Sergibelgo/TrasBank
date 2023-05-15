using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APITrassBank
{
    public class ScoringCreateDTO:LoanCreateDTO
    {
        [AllowNull]
        public float Deposit { get;set; }
        [Required]
        public int Persons { get; set; }
    }
}