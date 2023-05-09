using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class WorkerEditDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get;set; }
        [Required]
        [DataType(DataType.Password)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        public int WorkerStatusId { get; set; } = 1;
        [Required]
        public string FullName { get; set; }
    }
}
