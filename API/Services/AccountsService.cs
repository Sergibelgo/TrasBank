using APITrassBank.Context;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface IAccountsService
    {
        Task ChangeStatus(string id, int status);
        Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf);
        Task<Account> CreateMain(Customer customer);
        Task<IEnumerable<AccountResponseDTO>> GetAll();
        Task<AccountResponseDTO> GetById(string id, string userId = null);
        Task<Account> GetById(string id);
        Task<IEnumerable<AccountResponseDTO>> GetByUserId(string idSelf);
        Task<IEnumerable<AccountByUsernameDTO>> GetByUserName(string username);
        Task UpdateName(string name, string id, string idSelf);
    }
    public class AccountsService : IAccountsService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;

        public AccountsService(ContextDB contextDB, IMapper mapper)
        {
            _contextDB = contextDB;
            _mapper = mapper;
        }
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="model">DTO for new account</param>
        /// <param name="idSelf">Id of customer traying to create</param>
        /// <returns>New account</returns>
        /// <exception cref="ArgumentOutOfRangeException">If user id not found</exception>
        /// <exception cref="ArgumentException">If DTO date for saving is later than today</exception>
        public async Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf)
        {
            var user = _contextDB.Proyecto_Customers
                .Where(c => c.AppUser.Id == idSelf)
                .FirstOrDefault() ?? throw new ArgumentOutOfRangeException();
            if (model.AccountType == 2 && model.SaveUntil.CompareTo(DateTime.Now) < 0)
            {
                throw new ArgumentException("The saving date should be later than today");
            }
            var newAccount = new Account()
            {
                Customer = user,
                AccountName = model.AccountName,
                AccountStatusId = 1,
                AccountTypeId = model.AccountType,
                Balance = 0,
                SaveUntil = model.SaveUntil,
                Interest = 0
            };
            await _contextDB.Proyecto_Accounts.AddAsync(newAccount);
            await _contextDB.SaveChangesAsync();
            newAccount.AccountType=await _contextDB.Proyecto_AccountTypes.Where(x=>x.Id == model.AccountType).FirstOrDefaultAsync();
            newAccount.AccountStatus = new AccountStatus() { Id = 1, Description = "Enabled" };
            var result = _mapper.Map<AccountResponseDTO>(newAccount);
            return result;
        }
        /// <summary>
        /// Create the basic account for a new customer
        /// </summary>
        /// <param name="customer">New customer created</param>
        /// <returns>New account</returns>
        public async Task<Account> CreateMain(Customer customer)
        {
            var main = new Account()
            {
                Customer = customer,
                AccountName = "Main",
                AccountStatusId = 1,
                AccountTypeId = 1,
                Balance = 0,
                SaveUntil = DateTime.Now,
                Interest = 0
            };
            await _contextDB.Proyecto_Accounts.AddAsync(main);
            await _contextDB.SaveChangesAsync();
            return main;
        }
        /// <summary>
        /// Returns all accounts on db
        /// </summary>
        /// <returns>List of account</returns>
        public async Task<IEnumerable<AccountResponseDTO>> GetAll()
        {
            return await _contextDB.Proyecto_Accounts.Include(x => x.AccountStatus).Include(x => x.Customer).Include(x => x.AccountType).Select(x => _mapper.Map<AccountResponseDTO>(x)).ToListAsync();
        }
        /// <summary>
        /// Get account info by id and check if user is propetary if not null
        /// </summary>
        /// <param name="id">Id of account</param>
        /// <param name="userId">Propetary of account to check or null if doesnt matter</param>
        /// <returns>Account info</returns>
        /// <exception cref="ArgumentOutOfRangeException">Account id not found</exception>
        /// <exception cref="FieldAccessException">Propetary doesnt equals userId</exception>
        public async Task<AccountResponseDTO> GetById(string id, string userId = null)
        {
            var account = await _contextDB.Proyecto_Accounts.Include(x => x.Customer).ThenInclude(x => x.AppUser).Include(x => x.AccountStatus).Include(x => x.AccountType).Where(x => x.Id.ToString() == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            if (userId is not null)
            {
                if (account.Customer.AppUser.Id.ToString() != userId)
                {
                    throw new FieldAccessException();
                }
            }
            return _mapper.Map<AccountResponseDTO>(account);
        }
        /// <summary>
        /// Gets all account of customer by customer id
        /// </summary>
        /// <param name="userId">Customer id</param>
        /// <returns>List of account</returns>
        public async Task<IEnumerable<AccountResponseDTO>> GetByUserId(string userId)
        {

            return await _contextDB.Proyecto_Accounts
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.AccountStatus)
                .Include(x => x.AccountType)
                .Where(x => x.Customer.AppUser.Id == userId || x.Customer.Id.ToString()==userId)            
                .Select(x => _mapper.Map<AccountResponseDTO>(x)).ToListAsync();
        }
        /// <summary>
        /// Changes the name of account, needs to be propetary of it
        /// </summary>
        /// <param name="name">New name of the account</param>
        /// <param name="id">Id of account</param>
        /// <param name="idSelf">Id of the customer</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">If account doesnt belong to customer (idSelf!=account.customer.id)</exception>
        /// <exception cref="ArgumentOutOfRangeException">Account id not found</exception>
        public async Task UpdateName(string name, string id, string idSelf)
        {
            var user = await _contextDB.Proyecto_Customers.Where(x => x.AppUser.Id == idSelf).FirstOrDefaultAsync() ?? throw new ArgumentException();
            var account = await _contextDB.Proyecto_Accounts.Include(x => x.Customer).Where(x => x.Id.ToString() == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            if (account.Customer.Id.ToString() != user.Id.ToString())
            {
                throw new ArgumentException();
            }
            account.AccountName = name;
            await _contextDB.SaveChangesAsync();
        }
        /// <summary>
        /// Get an account by id
        /// </summary>
        /// <param name="id">Id of account</param>
        /// <returns>Account</returns>
        /// <exception cref="ArgumentOutOfRangeException">If account id is not found</exception>
        public async Task<Account> GetById(string id)
        {
            var account = await _contextDB.Proyecto_Accounts.Where(x => x.Id.ToString() == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            return account;
        }
        /// <summary>
        /// Get account list by username
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>List of {Id:number,Name:string} to use on a list selector</returns>
        /// <exception cref="ArgumentOutOfRangeException">If username not found</exception>
        public async Task<IEnumerable<AccountByUsernameDTO>> GetByUserName(string username)
        {
            var user = await _contextDB.Users.Where(x => x.UserName == username).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            return await _contextDB.Proyecto_Accounts
                .Where(x => x.Customer.AppUser.UserName == username)
                .Select(x => _mapper.Map<AccountByUsernameDTO>(x))
                .ToListAsync();
        }
        /// <summary>
        /// Changes the status of an account to x
        /// </summary>
        /// <param name="id">Id of account</param>
        /// <param name="status">Id of enum of type AccountStatus</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If status not found</exception>
        public async Task ChangeStatus(string id, int status)
        {
            var account=await _contextDB.Proyecto_Accounts.Where(x=>x.Id.ToString()==id).FirstOrDefaultAsync()??throw new ArgumentOutOfRangeException();
            var statusCheck = await _contextDB.Proyecto_AccountStatuses.Where(x=>x.Id==status).FirstOrDefaultAsync()??throw new ArgumentOutOfRangeException();
            account.AccountStatusId = status;
            await _contextDB.SaveChangesAsync();
        }
    }
}
