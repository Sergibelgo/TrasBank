using APITrassBank.Context;
using APITrassBank.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using Entitys.Entity;

namespace APITrassBank.Services
{
    public interface IWorkerService
    {
        Task<Worker> GetWorker(string Id);
        Task<IEnumerable<Worker>> GetWorkers();
        Task<Worker> NewWorker(WorkerRegisterDTO model);
        Task<Worker> UpdateWorker(WorkerEditDTO model, string id);
    }
    public class WorkerService : IWorkerService
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> userManager;

        public WorkerService(ContextDB contextDB, UserManager<IdentityUser> userManager)
        {
            this._contextDB = contextDB;
            this.userManager = userManager;
        }
        public async Task<IEnumerable<Worker>> GetWorkers()
        {
            var list = await _contextDB.Workers.Include(worker=>worker.AppUser).Select(worker => worker).ToListAsync();
            return list;
        }
        public async Task<Worker> GetWorker(string Id)
        {
            var worker = await _contextDB.Workers.Where(worker => worker.Id.ToString() == Id).FirstOrDefaultAsync();
            return worker;
        }
        public async Task<Worker> UpdateWorker(WorkerEditDTO model,string id)
        {
            var OldWorker = await _contextDB.Workers.Include(worker=>worker.AppUser).Where(worker=>worker.Id.ToString()==id).FirstOrDefaultAsync();
            if (OldWorker is null)
            {
                return null;
            }
            OldWorker.AppUser.UserName= model.Username;
            OldWorker.AppUser.NormalizedUserName = model.Username.ToUpper();
            OldWorker.AppUser.Email = model.Email;
            OldWorker.AppUser.NormalizedEmail = model.Email.ToUpper();
            OldWorker.StartDate = model.StartDate;
            OldWorker.Name = model.FullName;
            OldWorker.WorkerStatus = await _contextDB.WorkerStatuses.FindAsync(model.WorkerStatusId);
            var result =  _contextDB.Workers.Update(OldWorker).Entity;
            await _contextDB.SaveChangesAsync();
            return result;
        }
        public async Task<Worker> NewWorker(WorkerRegisterDTO model)
        {
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            IdentityUser newUser = new()
            {
                Email = model.Email,
                UserName = model.Username
            };
            var response = await userManager.CreateAsync(newUser, password: model.Password);
            if (response.Succeeded)
            {
                var user = (await userManager.FindByEmailAsync(newUser.Email));
                await userManager.AddToRoleAsync(user, "Worker");
                var workerStatus = await _contextDB.WorkerStatuses.FirstAsync(ws => ws.Id == model.WorkerStatusId);
                Worker newWorker = new()
                {
                    Name = model.FullName,
                    AppUser = user,
                    StartDate= model.StartDate,
                    WorkerStatus=workerStatus
                };
                try
                {
                    var worker = await _contextDB.Workers.AddAsync(newWorker);
                    await _contextDB.SaveChangesAsync();
                    scope.Complete();
                    return worker.Entity;
                }
                catch
                {
                    scope.Dispose();
                    return null;
                }

            }
            else
            {
                scope.Dispose();
                return null;
            }
        }
    }
}
