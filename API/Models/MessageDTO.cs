using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class MessageDTO
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public string SenderName { get; set; }
        public string SenderId { get; set; }

        public bool IsReaded { get; set; }
    }
}
