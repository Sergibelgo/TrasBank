namespace APITrassBank
{
    public class PaymentResponseDTO
    {
        public Guid Id { get; set; }
        public decimal Ammount { get; set; }
        public DateTime Date { get; set; }
        public Guid LoanId { get; set; }
    }
}