using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class CustomerEditDTO:UserEditDTO
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
        [Required]
        public int WorkStatusId { get; set; }
        [Required]
        public Guid WorkerId { get; set; }
    }
}
