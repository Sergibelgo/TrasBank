using APITrassBank.Context;
using APITrassBank.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank
{
    public interface ITransactionsService
    {
        Task AddMoney(int quantity, string idSelf, string idAccount);
        Task<IEnumerable<TransactionResponseDTO>> GetByUserId(string id);
        Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf, string id);
    }
    public class TransactionsService : ITransactionsService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;
        private readonly IAccountsService _accountsService;
        private readonly IEnumsService _enumsService;

        public TransactionsService(ContextDB contextDB, IMapper mapper, IAccountsService accountsService, IEnumsService enumsService)
        {
            _contextDB = contextDB;
            _mapper = mapper;
            _accountsService = accountsService;
            _enumsService = enumsService;
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf, string id)
        {
            return await _contextDB.Transactions
                 .Include(x => x.Account)
                 .ThenInclude(x => x.Customer)
                 .Include(x => x.OtherInvolved)
                 .Include(x => x.TransactionType)
                 .Where(x => x.Account.Customer.AppUser.Id == idSelf && x.Account.Id.ToString() == id)
                 .Select(x => _mapper.Map<TransactionResponseDTO>(x))
                 .ToListAsync();
        }
        public async Task<IEnumerable<TransactionResponseDTO>> GetByUserId(string id)
        {
            return await _contextDB.Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Customer)
                .Where(x => x.Account.Customer.Id.ToString() == id)
                .Select(x => _mapper.Map<TransactionResponseDTO>(x))
                .ToListAsync();
        }

        public async Task AddMoney(int quantity, string idSelf, string idAccount)
        {
            var user = await _contextDB.Users.Where(x => x.Id == idSelf).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var account = await _accountsService.GetById(idAccount);
            var transacction = await _enumsService.GetTranssactionTypeAsync(3);
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            account.Balance += quantity;
            var transaction = new Entitys.Entity.Transaction()
            {
                Account = account,
                Ammount = quantity,
                Date = DateTime.UtcNow,
                OtherInvolved = user,
                TransactionType = transacction
            };
            await _contextDB.AddAsync(transaction);
            _contextDB.Update(account);
            await _contextDB.SaveChangesAsync();
            scope.Complete();
        }
    }
}