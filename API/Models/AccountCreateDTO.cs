using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace APITrassBank
{
    public class AccountCreateDTO
    {
        [Required]
        public string AccountName { get; set; }
        [AllowNull]
        public DateTime SaveUntil { get; set; }
        [Required]
        public int AccountType { get; set; }
    }
}