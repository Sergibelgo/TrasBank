namespace APITrassBank
{
    public class PaymentCreateDTO
    {
        public string AccountId { get; set; }
        public string LoanId { get; set; }
        public int NumberInstallments { get; set; }
    }
}