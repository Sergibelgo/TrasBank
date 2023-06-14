namespace APITrassBank
{
    public class TransactionResponseDTO
    {

        public string Id { get; set; }
        public string TipeTransaction { get; set; }
        public decimal Ammount { get; set; }
        public DateTime Date { get; set; }
        public string NameOther { get; set; } = null;
        public string AccountName { get; set; } = null;
        public string Concept { get;set; }
    }
}