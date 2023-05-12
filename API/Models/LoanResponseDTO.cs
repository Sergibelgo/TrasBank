using Entitys.Entity;
using System.ComponentModel.DataAnnotations;

namespace APITrassBank.Models
{
    public class LoanResponseDTO
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public decimal Ammount { get; set; }
        public int InterestRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalInstallments { get; set; }
        public int RemainingInstallments { get; set; }
        public decimal RemainingAmmount { get; set; }
        public string LoanStatus { get; set; }
        public string LoanType { get; set; }
    }
}
