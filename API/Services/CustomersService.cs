using APITrassBank.Context;
using APITrassBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Entitys.Entity;

namespace APITrassBank.Services
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(CustomerRegisterDTO model);
        Task<Customer> GetCustomerAsync(string id);
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<bool> IsValidModel(CustomerEditDTO model);
        Task<Customer> UpdateCustomer(Customer user, CustomerEditDTO model);
        Task<Customer> UpdateSelf(CustomerEditSelfDTO model, Customer customer);
    }
    public class CustomersService : ICustomerService
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEnumsService _enums;

        public CustomersService(ContextDB contextDB, UserManager<IdentityUser> userManager, IEnumsService enums)
        {
            _contextDB = contextDB;
            _userManager = userManager;
            _enums = enums;
        }
        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            var customers = await _contextDB.Customers.Include(customer => customer.AppUser)
                                                .Include(customer => customer.Worker)
                                                .Include(customer => customer.WorkStatus)
                                                .Select(customer => customer).ToListAsync();
            return customers;
        }
        public async Task<Customer> GetCustomerAsync(string id)
        {
            var customer = await _contextDB.Customers
                                                .Where(customer => customer.Id.ToString() == id || customer.AppUser.Id == id)
                                                .Include(customer => customer.AppUser)
                                                .Include(customer => customer.Worker)
                                                .Include(customer => customer.WorkStatus)
                                                .Select(customer => customer)
                                                .FirstOrDefaultAsync();
            return customer;
        }
        public async Task<Customer> CreateCustomer(CustomerRegisterDTO model)
        {
            var worker = await _contextDB.Workers.Where(worker => worker.Id.ToString() == model.WorkerId).FirstOrDefaultAsync();
            var workingStatus = await _enums.GetCustomerWorkingStatusAsync(model.WorkStatusId);
            if (worker is null || workingStatus is null)
            {
                return null;
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
                    var customer = await _contextDB.Customers.AddAsync(newCustomer);
                    await _contextDB.SaveChangesAsync();
                    scope.Complete();
                    return customer.Entity;
                }
                else
                {
                    scope.Dispose();
                    return null;
                }
            }
            catch
            {
                scope.Dispose();
                return null;
            }
        }

        public async Task<bool> IsValidModel(CustomerEditDTO model)
        {
            if (model == null)
            {
                return false;
            }
            var worker = await _contextDB.Workers.FirstOrDefaultAsync(w => w.Id == model.WorkerId);
            if (worker is null)
            {
                return false;
            }
            var workStatus = await _contextDB.WorkingStates.FirstOrDefaultAsync(ws => ws.Id == model.WorkStatusId);
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
                customer.Worker = await _contextDB.Workers.FirstOrDefaultAsync(w => w.Id == model.WorkerId);
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
    }
}
