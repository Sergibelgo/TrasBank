namespace APITrassBank.Models
{
    public class CustomerRegisterDTO:UserRegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime Age { get; set; }
        public decimal Income { get; set; }
        public int WorkStatusId { get; set; }
        public string WorkerEmail { get; set; }
    }
}
