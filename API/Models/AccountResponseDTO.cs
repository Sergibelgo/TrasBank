namespace APITrassBank
{
    public class AccountResponseDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime SaveUntil { get; set; }
        public string Status { get; set; }
        public decimal Balance { get; set; }
        public string Type { get; set; }
        public decimal Interest { get; set; }
        public string Propetary { get; set; }
        public string PropetaryId { get; set; }
    }
}