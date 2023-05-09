using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class WorkerStatus
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
