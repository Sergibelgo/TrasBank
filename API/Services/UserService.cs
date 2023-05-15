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
            if (!(await _contextDB.WorkerStatuses.AnyAsync()))
            {
                _contextDB.WorkerStatuses.Add(new WorkerStatus() { Name = "Up" });
                _contextDB.WorkerStatuses.Add(new WorkerStatus() { Name = "Down" });
            }
            if (!(await _contextDB.WorkingStates.AnyAsync()))
            {
                _contextDB.WorkingStates.Add(new CustomerWorkingStatus() { Name = "Working" });
                _contextDB.WorkingStates.Add(new CustomerWorkingStatus() { Name = "Unemployed" });
            }
            if(!(await _contextDB.LoanStatuses.AnyAsync()))
            {
                _contextDB.LoanStatuses.Add(new LoanStatus() { Name = "Waiting" });
                _contextDB.LoanStatuses.Add(new LoanStatus() { Name = "Aproved" });
                _contextDB.LoanStatuses.Add(new LoanStatus() { Name = "Denied" });
            }
            if (!(await _contextDB.LoansTypes.AnyAsync()))
            {
                _contextDB.LoansTypes.Add(new LoanType() { Name = "Personal" });
                _contextDB.LoansTypes.Add(new LoanType() { Name = "Buissness" });
                _contextDB.LoansTypes.Add(new LoanType() { Name = "Home" });
            }
            if (!(await _contextDB.AccountStatuses.AnyAsync()))
            {
                _contextDB.AccountStatuses.Add(new AccountStatus() { Description = "Enabled" });
                _contextDB.AccountStatuses.Add(new AccountStatus() { Description = "Blocked" });
                _contextDB.AccountStatuses.Add(new AccountStatus() { Description = "Disabled" });
            }
            if (!(await _contextDB.TranssactionTypes.AnyAsync()))
            {
                _contextDB.TranssactionTypes.Add(new TranssactionType() { Name = "Add" });
                _contextDB.TranssactionTypes.Add(new TranssactionType() { Name = "Draw" });
                _contextDB.TranssactionTypes.Add(new TranssactionType() { Name = "Transfer" });
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
            var tryLog=await _userManager.CheckPasswordAsync(user,model.Password);
            if (!tryLog)
            {
                throw new Exception();
            }
            return user;
        }
        public async Task<bool> ChangePassword(UserPasswordDTO model,string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return false;
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPass, model.NewPass);
            return result.Succeeded;
        }
    }
}
