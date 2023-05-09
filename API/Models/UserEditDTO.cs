using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class UserEditDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
