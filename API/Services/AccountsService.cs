﻿using APITrassBank.Context;
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

        public async Task<IEnumerable<AccountResponseDTO>> GetAll()
        {
            return await _contextDB.Proyecto_Accounts.Include(x => x.AccountStatus).Include(x => x.Customer).Include(x => x.AccountType).Select(x => _mapper.Map<AccountResponseDTO>(x)).ToListAsync();
        }

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
        public async Task<Account> GetById(string id)
        {
            var account = await _contextDB.Proyecto_Accounts.Where(x => x.Id.ToString() == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            return account;
        }

        public async Task<IEnumerable<AccountByUsernameDTO>> GetByUserName(string username)
        {
            var user = await _contextDB.Users.Where(x => x.UserName == username).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            return await _contextDB.Proyecto_Accounts
                .Where(x => x.Customer.AppUser.UserName == username)
                .Select(x => _mapper.Map<AccountByUsernameDTO>(x))
                .ToListAsync();
        }

        public async Task ChangeStatus(string id, int status)
        {
            var account=await _contextDB.Proyecto_Accounts.Where(x=>x.Id.ToString()==id).FirstOrDefaultAsync()??throw new ArgumentOutOfRangeException();
            var statusCheck = await _contextDB.Proyecto_AccountStatuses.Where(x=>x.Id==status).FirstOrDefaultAsync()??throw new ArgumentOutOfRangeException();
            account.AccountStatusId = status;
            await _contextDB.SaveChangesAsync();
        }
    }
}
