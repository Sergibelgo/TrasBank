using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class WorkerStatus
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
