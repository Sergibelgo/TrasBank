using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class UserLoginDTO
    {
        public string Username { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
