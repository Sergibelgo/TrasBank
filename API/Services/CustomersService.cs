using APITrassBank.Context;
using APITrassBank.Models;
using AutoMapper;
using Azure;
using Entitys.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(CustomerRegisterDTO model);
        Task<Customer> GetCustomerAsync(string id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<IEnumerable<CustomerSelfDTO>> GetCustomersSelfWorkerAsync(string idSelf);
        Task<CustomerSelfDTO> GetSelfAsync(string id);
        Task<bool> IsValidModel(CustomerEditDTO model);
        Task<Customer> UpdateCustomer(Customer user, CustomerEditDTO model);
        Task<Customer> UpdateSelf(CustomerEditSelfDTO model, Customer customer);
    }
    public class CustomersService : ICustomerService
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEnumsService _enums;
        private readonly IAccountsService _accountsService;
        private readonly IMapper _mapper;

        public CustomersService(ContextDB contextDB, UserManager<IdentityUser> userManager, IEnumsService enums, IAccountsService accountsService, IMapper mapper)
        {
            _contextDB = contextDB;
            _userManager = userManager;
            _enums = enums;
            _accountsService = accountsService;
            _mapper = mapper;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = await _contextDB.Proyecto_Customers.Include(customer => customer.AppUser)
                                                .Include(customer => customer.Worker)
                                                .Include(customer => customer.WorkStatus)
                                                .Select(customer => customer).ToListAsync();
            return customers;
        }
        public async Task<Customer> GetCustomerAsync(string id)
        {
            var customer = await _contextDB.Proyecto_Customers
                                                .Where(customer => customer.Id.ToString() == id || customer.AppUser.Id == id)
                                                .Include(customer => customer.AppUser)
                                                .Include(customer => customer.Worker)
                                                .Include(customer => customer.WorkStatus)
                                                .Select(customer => customer)
                                                .FirstOrDefaultAsync();
            return customer;
        }
        public async Task<CustomerSelfDTO> GetSelfAsync(string id)
        {
            var customer = await _contextDB.Proyecto_Customers
                .Include(a => a.AppUser)
                .Where(c => c.AppUser.Id == id)
                .Select(a => _mapper.Map<CustomerSelfDTO>(a))
                .FirstOrDefaultAsync();
            var accounts = await _contextDB.Proyecto_Accounts
                .Where(a => a.Customer.AppUser.Id == id)
                .Select(a => new AccountList
                {
                    Id = a.Id.ToString(),
                    Name = a.AccountName
                })
                .ToListAsync();
            customer.Accounts = accounts;
            return customer;
        }
        public async Task<Customer> CreateCustomer(CustomerRegisterDTO model)
        {
            var worker = await _contextDB.Proyecto_Workers.Where(worker => worker.AppUser.NormalizedEmail == model.WorkerEmail.ToUpper()).FirstOrDefaultAsync();
            var workingStatus = await _enums.GetCustomerWorkingStatusAsync(model.WorkStatusId);
            if (worker is null || workingStatus is null)
            {
                throw new Exception("Invalid worker or working status");
            }
            if (model.Income < 0)
            {
                throw new ArgumentException("Invalid income");
            }
            if (model.Age.CompareTo(DateTime.Now.AddYears(-150))<0)
            {
                throw new ArgumentException("Invalid birthday");
            }
            DateTime date = DateTime.Now;
            date = date.AddYears(-18);
            if (model.Age.CompareTo(date) > 0)
            {
                throw new Exception("The client must be over 18 years old");
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                IdentityUser newUser = new()
                {
                    Email = model.Email,
                    UserName = model.Username
                };
                var response = await _userManager.CreateAsync(newUser, password: model.Password);
                if (response.Succeeded)
                {
                    var user = (await _userManager.FindByEmailAsync(newUser.Email));
                    Customer newCustomer = new()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Address = model.Address,

                        Age = model.Age,
                        Income = model.Income,
                        Worker = worker,
                        WorkStatusId = model.WorkStatusId,
                        AppUser = user
                    };
                    var customer = await _contextDB.Proyecto_Customers.AddAsync(newCustomer);
                    await _contextDB.SaveChangesAsync();
                    await _accountsService.CreateMain(customer.Entity);
                    scope.Complete();
                    return customer.Entity;
                }
                else
                {
                    scope.Dispose();
                    throw new ArgumentException(string.Join(" ",response.Errors.Select(x=>x.Description)));
                }
            }
            catch (Exception ex) 
            {
                scope.Dispose();
                throw new ArgumentException(ex.Message);
            }
        }

        public async Task<bool> IsValidModel(CustomerEditDTO model)
        {
            if (model == null)
            {
                return false;
            }
            var worker = await _contextDB.Proyecto_Workers.FirstOrDefaultAsync(w => w.Id == model.WorkerId);
            if (worker is null)
            {
                return false;
            }
            var workStatus = await _contextDB.Proyecto_WorkingStates.FirstOrDefaultAsync(ws => ws.Id == model.WorkStatusId);
            if (workStatus is null)
            {
                return false;
            }
            var diffDate = (DateTime.Now - model.Age).Days / 365;
            if (diffDate > 150)
            {
                return false;
            }
            return true;
        }

        public async Task<Customer> UpdateCustomer(Customer customer, CustomerEditDTO model)
        {

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                customer.AppUser.Email = model.Email;
                customer.AppUser.UserName = model.Username;
                await _userManager.UpdateAsync(customer.AppUser);
                customer.FirstName = model.FirstName;
                customer.LastName = model.LastName;
                customer.Address = model.Address;
                customer.Age = model.Age;
                customer.Income = model.Income;
                customer.WorkStatusId = model.WorkStatusId;
                customer.Worker = await _contextDB.Proyecto_Workers.FirstOrDefaultAsync(w => w.Id == model.WorkerId);
                await _contextDB.SaveChangesAsync();
                scope.Complete();
                return customer;
            }
            catch
            {
                scope.Dispose();
                return null;
            }
        }
        public async Task<Customer> UpdateSelf(CustomerEditSelfDTO model, Customer customer)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            try
            {
                customer.AppUser.Email = model.Email;
                customer.AppUser.UserName = model.Username;
                await _userManager.UpdateAsync(customer.AppUser);
                customer.Income = model.Income;
                customer.Address = model.Address;
                customer.Age = model.Age;
                customer.LastName = model.LastName;
                customer.FirstName = model.FirstName;
                await _contextDB.SaveChangesAsync();
                scope.Complete();
                return customer;
            }
            catch
            {
                scope.Dispose();
                return null;
            }
        }

        public async Task<IEnumerable<CustomerSelfDTO>> GetCustomersSelfWorkerAsync(string idSelf)
        {
            var customers = await _contextDB.Proyecto_Customers.Include(customer => customer.AppUser)
                                                 .Include(customer => customer.Worker)
                                                 .Include(customer => customer.WorkStatus)
                                                 .Where(customer=>customer.Worker.AppUser.Id==idSelf)
                                                 .Select(customer => _mapper.Map<CustomerSelfDTO>(customer)).ToListAsync();
            return customers;
        }
    }
}
