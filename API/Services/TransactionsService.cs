using APITrassBank.Context;
using APITrassBank.Services;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank
{
    public interface ITransactionsService
    {
        Task AddorRemoveMoney(decimal quantity, string idSelf, string idAccount, string concept = "");
        Task<IEnumerable<TransactionResponseDTO>> GetByUserId(string id);
        Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf, string id);
        Task TransferTo(TransferMoneyDTO model, string idSelf);
    }
    public class TransactionsService : ITransactionsService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;
        private readonly IAccountsService _accountsService;
        private readonly IEnumsService _enumsService;
        private readonly IMessagesService _messagesService;

        public TransactionsService(ContextDB contextDB, IMapper mapper, IAccountsService accountsService, IEnumsService enumsService, IMessagesService messagesService)
        {
            _contextDB = contextDB;
            _mapper = mapper;
            _accountsService = accountsService;
            _enumsService = enumsService;
            _messagesService = messagesService;
        }
        /// <summary>
        /// Get all transaction of an account
        /// </summary>
        /// <param name="idSelf">Id of user</param>
        /// <param name="id">Id of account</param>
        /// <returns>List of transactions</returns>
        public async Task<IEnumerable<TransactionResponseDTO>> GetSelf(string idSelf, string id)
        {
            return await _contextDB.Proyecto_Transactions
                 .Include(x => x.Account)
                 .ThenInclude(x => x.Customer)
                 .Include(x => x.OtherInvolved)
                 .Include(x => x.TransactionType)
                 .Where(x => x.Account.Customer.AppUser.Id == idSelf && x.Account.Id.ToString() == id)
                 .Select(x => _mapper.Map<TransactionResponseDTO>(x))
                 .ToListAsync();
        }
        /// <summary>
        /// Get all transacctions done by user with his id
        /// </summary>
        /// <param name="id">Id of customer</param>
        /// <returns>List of transactions</returns>
        public async Task<IEnumerable<TransactionResponseDTO>> GetByUserId(string id)
        {
            return await _contextDB.Proyecto_Transactions
                .Include(x => x.Account)
                .ThenInclude(x => x.Customer)
                .Include(x => x.TransactionType)
                .Include(x => x.OtherInvolved)
                .Where(x => x.Account.Customer.AppUser.Id == id|| x.Account.Customer.Id.ToString()==id)
                .Select(x => _mapper.Map<TransactionResponseDTO>(x))
                .ToListAsync();
        }
        /// <summary>
        /// Method that adds or remove money from an account
        /// </summary>
        /// <param name="quantity">Quantity of operation</param>
        /// <param name="idSelf">Id user doing the operation</param>
        /// <param name="idAccount">Id of account</param>
        /// <param name="concept">Concept of the operation</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If user or account not found</exception>
        /// <exception cref="ArgumentException">If founds are insufficient</exception>
        public async Task AddorRemoveMoney(decimal quantity, string idSelf, string idAccount, string concept = "")
        {
            var user = await _contextDB.Users.Where(x => x.Id == idSelf).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var account = await _accountsService.GetById(idAccount);
            var transacction = await _enumsService.GetTranssactionTypeAsync((quantity < 0) ? 2 : 1);
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            account.Balance += quantity;
            if (account.Balance < 0)
            {
                throw new ArgumentException("Insufficient funds");
            }
            var transaction = new Entitys.Entity.Transaction()
            {
                Account = account,
                Ammount = quantity,
                Date = DateTime.UtcNow,
                OtherInvolved = user,
                TransactionType = transacction,
                Concept = concept
            };
            await _contextDB.AddAsync(transaction);
            _contextDB.Update(account);
            await _contextDB.SaveChangesAsync();
            scope.Complete();
        }
        /// <summary>
        /// Method to transfer money from an account to another
        /// </summary>
        /// <param name="model">DTO with necesary info</param>
        /// <param name="idSelf">Id user doing the transfer</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If either user or account is not found</exception>
        /// <exception cref="ArgumentException">If account is the same, the account is not enabled, is saving account with not required dates or is invalid quantity</exception>
        public async Task TransferTo(TransferMoneyDTO model, string idSelf)
        {
            var user = await _contextDB.Users
                .Where(x => x.Id == idSelf)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var account = await _contextDB.Proyecto_Accounts
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Where(x => x.Id.ToString() == model.AccountReciverId)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var accountSelf = await _contextDB.Proyecto_Accounts
                .Include(x=>x.Customer)
                .Where(x => x.Id.ToString() == model.AccountSenderId && x.Customer.AppUser.Id == idSelf)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            if (accountSelf.Id.Equals(account.Id))
            {
                throw new ArgumentException($"The account must be diferent");
            }
            if (accountSelf.AccountStatusId != 1 || account.AccountStatusId != 1)
            {
                throw new ArgumentException($"The account is {accountSelf.AccountStatus.Description}");
            }
            if (accountSelf.AccountTypeId == 2 && accountSelf.SaveUntil.CompareTo(DateTime.Now) > 0)
            {
                throw new ArgumentException("The saving account does not meet the date yet");
            }
            if (accountSelf.Balance < model.Quantity)
            {
                throw new ArgumentException("Insufficient funds");
            }
            if (model.Quantity < 0)
            {
                throw new ArgumentException("Invalid quantity");
            }
            var transaction = await _enumsService.GetTranssactionTypeAsync(3);
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            account.Balance += model.Quantity;
            accountSelf.Balance -= model.Quantity;
            var transactionAdd = new Entitys.Entity.Transaction()
            {
                Account = account,
                Ammount = Math.Abs(model.Quantity),
                Date = DateTime.UtcNow,
                OtherInvolved = user,
                TransactionType = transaction,
                Concept = model.Concept ?? ""
            };
            var transactionSub = new Entitys.Entity.Transaction()
            {
                Account = accountSelf,
                Ammount = -(Math.Abs(model.Quantity)),
                Date = DateTime.UtcNow,
                OtherInvolved = account.Customer.AppUser,
                TransactionType = transaction,
                Concept = model.Concept ?? ""
            };
            await _contextDB.AddRangeAsync(transactionSub, transactionAdd);
            _contextDB.UpdateRange(account, accountSelf);
            await _contextDB.SaveChangesAsync();
            await _messagesService.Create(idSelf, new Models.MessageCreateDTO()
            {
                Title = "New transfer",
                Body = $"{user.UserName} made a new transfer for ${Math.Abs(model.Quantity)}",
                ReciverUserName = account.Customer.AppUser.UserName
            });
            scope.Complete();
        }
    }
}