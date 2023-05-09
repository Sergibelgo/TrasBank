using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class WorkerRegisterDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get;set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get;set; }
        public DateTime StartDate { get; set; } 
        public int WorkerStatusId { get; set; } = 1;
        [Required]
        public string FullName { get; set; }
    }
}
