using APITrassBank.Context;
using APITrassBank.Models;
using Entitys.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface IUserService
    {
        Task<bool> ChangePassword(UserPasswordDTO model, string id);
        Task GenerateBasics();
        Task<IdentityUser> GetUser(string id);
        Task<IdentityUser> GetUserByUserName(string userName);
        Task<IEnumerable<IdentityUser>> GetUsers();
        Task<IdentityUser> LogIn(UserLoginDTO model);
    }
    public class UserService : IUserService
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(ContextDB contextDB, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _contextDB = contextDB;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<IEnumerable<IdentityUser>> GetUsers()
        {
            return await _userManager.Users.ToListAsync();
        }
        public async Task<IdentityUser> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return user;
        }
        public async Task GenerateBasics()
        {
            if (!(await _roleManager.Roles.AnyAsync()))
            {
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Admin"
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "Worker"
                });
                await _roleManager.CreateAsync(new IdentityRole
                {
                    Name = "ATM"
                });
            }
            if (!(await _userManager.Users.AnyAsync()))
            {
                IdentityUser user = new()
                {
                    Email = "sergibelgo@gmail.com",
                    UserName = "sergibelgo"
                };
                await _userManager.CreateAsync(user, password: "aA123456!");
                await _userManager.AddToRoleAsync(user, "Admin");

            }
            if (!(await _contextDB.Proyecto_WorkerStatuses.AnyAsync()))
            {
                _contextDB.Proyecto_WorkerStatuses.Add(new WorkerStatus() { Name = "Up" });
                _contextDB.Proyecto_WorkerStatuses.Add(new WorkerStatus() { Name = "Down" });
            }
            if (!(await _contextDB.Proyecto_WorkingStates.AnyAsync()))
            {
                _contextDB.Proyecto_WorkingStates.Add(new CustomerWorkingStatus() { Name = "Working" });
                _contextDB.Proyecto_WorkingStates.Add(new CustomerWorkingStatus() { Name = "Unemployed" });
            }
            if (!(await _contextDB.Proyecto_LoanStatuses.AnyAsync()))
            {
                _contextDB.Proyecto_LoanStatuses.Add(new LoanStatus() { Name = "Waiting" });
                _contextDB.Proyecto_LoanStatuses.Add(new LoanStatus() { Name = "Aproved" });
                _contextDB.Proyecto_LoanStatuses.Add(new LoanStatus() { Name = "Denied" });
                _contextDB.Proyecto_LoanStatuses.Add(new LoanStatus() { Name = "Paid" });
            }
            if (!(await _contextDB.Proyecto_LoansTypes.AnyAsync()))
            {
                _contextDB.Proyecto_LoansTypes.Add(new LoanType() { Name = "Personal",Percentaje=20,TAE=2,TIN=1 });
                _contextDB.Proyecto_LoansTypes.Add(new LoanType() { Name = "Buissness",Percentaje=30,TAE=2,TIN=1 });
                _contextDB.Proyecto_LoansTypes.Add(new LoanType() { Name = "Home" ,Percentaje=40,TAE=3,TIN=1});
            }
            if (!(await _contextDB.Proyecto_AccountStatuses.AnyAsync()))
            {
                _contextDB.Proyecto_AccountStatuses.Add(new AccountStatus() { Description = "Enabled" });
                _contextDB.Proyecto_AccountStatuses.Add(new AccountStatus() { Description = "Blocked" });
                _contextDB.Proyecto_AccountStatuses.Add(new AccountStatus() { Description = "Disabled" });
            }
            if (!(await _contextDB.Proyecto_AccountTypes.AnyAsync()))
            {
                _contextDB.Proyecto_AccountTypes.Add(new AccountType()
                {
                    Name = "Normal"
                });
                _contextDB.Proyecto_AccountTypes.Add(new AccountType()
                {
                    Name = "Savings"
                });
            }
            if (!(await _contextDB.Proyecto_TranssactionTypes.AnyAsync()))
            {
                _contextDB.Proyecto_TranssactionTypes.Add(new TranssactionType() { Name = "Add" });
                _contextDB.Proyecto_TranssactionTypes.Add(new TranssactionType() { Name = "Draw" });
                _contextDB.Proyecto_TranssactionTypes.Add(new TranssactionType() { Name = "Transfer" });
                _contextDB.Proyecto_TranssactionTypes.Add(new TranssactionType() { Name = "Loan Approved" });
            }
            await _contextDB.SaveChangesAsync();
        }
        public async Task<IdentityUser> LogIn(UserLoginDTO model)
        {
            IdentityUser user;
            if (!String.IsNullOrEmpty(model.Username))
            {
                user = await _userManager.FindByNameAsync(model.Username);
            }
            else
            {
                user = await _userManager.FindByEmailAsync(model.Email);
            }
            if (user == null)
            {
                throw new Exception();
            }
            var tryLog = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!tryLog)
            {
                throw new Exception();
            }
            return user;
        }
        public async Task<bool> ChangePassword(UserPasswordDTO model, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return false;
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPass, model.NewPass);
            return result.Succeeded;
        }

        public async Task<IdentityUser> GetUserByUserName(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            return user;
        }
    }
}
