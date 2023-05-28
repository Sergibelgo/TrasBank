﻿namespace APITrassBank.Models
{
    public class CustomerSelfDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public decimal Income { get; set; }
        public IEnumerable<AccountList> Accounts { get; set; }
        public DateTime Age { get; set; }
    }
}
