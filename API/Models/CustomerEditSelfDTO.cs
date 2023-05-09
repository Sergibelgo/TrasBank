using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class CustomerEditSelfDTO:UserEditDTO
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Age { get; set; }
        [Required]
        public decimal Income { get; set; }
    }
}
