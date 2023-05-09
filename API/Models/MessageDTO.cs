using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class MessageDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string ReciverName { get; set; }
        public string RevicerId { get; set; }

        public bool IsReaded { get; set; }
    }
}
