using APITrassBank.Context;

namespace APITrassBank
{
    public interface ITransactionsService
    {
        Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf);
    }
    public class TransactionsService : ITransactionsService
    {
        private readonly ContextDB _contextDB;

        public TransactionsService(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }

        public Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf)
        {
           return _contextDB.Transactions.Where(x=>x.Account.Customer.AppUser.Id==idSelf).Select(x=>_);
        }
    }
}