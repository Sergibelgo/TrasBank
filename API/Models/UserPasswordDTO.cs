using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class UserPasswordDTO
    {
        [Required]
        [DataType(DataType.Password)]
        public string OldPass { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPass { get; set; }
    }
}
