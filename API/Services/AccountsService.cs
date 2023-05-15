using APITrassBank.Context;
using Entitys.Entity;
using System.ComponentModel.Design;

namespace APITrassBank.Services
{
    public interface IAccountsService
    {
        Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf);
        Task<IEnumerable<AccountResponseDTO>> GetAll();
        Task<AccountResponseDTO> GetById(string id, string userId = null);
        Task<IEnumerable<AccountResponseDTO>> GetByUserId();
    }
    public class AccountsService : IAccountsService
    {
        private readonly ContextDB _contextDB;
        private readonly IEnumsService _enums;
        private readonly IResourceService resource;

        public AccountsService(ContextDB contextDB, IEnumsService enums, IResourceService resource)
        {
            _contextDB = contextDB;
            _enums = enums;
            this.resource = resource;
        }
        public async Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf)
        {
            var user = _contextDB.Customers.Where(c => c.AppUser.Id.ToString() == idSelf).FirstOrDefault() ?? throw new ArgumentOutOfRangeException();
            var newAccount = new Account()
            {
                Customer = user,
                AccountName = model.AccountName,
                AccountStatusId = 1,
                AccountTypeId = model.AccountType,
                Balance = 0,
                SaveUntil = model.SaveUntil,

            };
            return null;
        }

        public Task<IEnumerable<AccountResponseDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponseDTO> GetById(string id, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponseDTO>> GetByUserId()
        {
            throw new NotImplementedException();
        }
    }
}
