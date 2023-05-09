using System.ComponentModel.DataAnnotations;

namespace Entitys.Entity
{
    public class TranssactionType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
