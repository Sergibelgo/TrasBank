﻿using APITrassBank.Context;
using APITrassBank.Models;
using Entitys.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank.Services
{
    public interface IWorkerService
    {
        Task<Worker> GetWorker(string Id);
        Task<IEnumerable<Worker>> GetWorkers();
        Task<IEnumerable<WorkersMailsDTO>> GetWorkersMail();
        Task<Worker> NewWorker(WorkerRegisterDTO model);
        Task<Worker> UpdateWorker(WorkerEditDTO model, string id);
    }
    public class WorkerService : IWorkerService
    {
        private readonly ContextDB _contextDB;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IEnumsService _enums;

        public WorkerService(ContextDB contextDB, UserManager<IdentityUser> userManager, IEnumsService enums)
        {
            this._contextDB = contextDB;
            this.userManager = userManager;
            _enums = enums;
        }
        /// <summary>
        /// Get all workers
        /// </summary>
        /// <returns>List of workers</returns>
        public async Task<IEnumerable<Worker>> GetWorkers()
        {
            var list = await _contextDB.Proyecto_Workers.Include(worker => worker.AppUser).Select(worker => worker).ToListAsync();
            return list;
        }
        /// <summary>
        /// Get worker by id
        /// </summary>
        /// <param name="Id">Id of worker</param>
        /// <returns>Worker or null</returns>
        public async Task<Worker> GetWorker(string Id)
        {
            var worker = await _contextDB.Proyecto_Workers.Where(worker => worker.Id.ToString() == Id).FirstOrDefaultAsync();
            return worker;
        }
        /// <summary>
        /// Update the information of the worker
        /// </summary>
        /// <param name="model">DTO</param>
        /// <param name="id">Id of worker</param>
        /// <returns>Worker updated</returns>
        public async Task<Worker> UpdateWorker(WorkerEditDTO model, string id)
        {
            var OldWorker = await _contextDB.Proyecto_Workers.Include(worker => worker.AppUser).Where(worker => worker.Id.ToString() == id).FirstOrDefaultAsync();
            if (OldWorker is null)
            {
                return null;
            }
            OldWorker.AppUser.UserName = model.Username;
            OldWorker.AppUser.NormalizedUserName = model.Username.ToUpper();
            OldWorker.AppUser.Email = model.Email;
            OldWorker.AppUser.NormalizedEmail = model.Email.ToUpper();
            OldWorker.StartDate = model.StartDate;
            OldWorker.Name = model.FullName;
            OldWorker.WorkerStatus = await _contextDB.Proyecto_WorkerStatuses.FindAsync(model.WorkerStatusId);
            var result = _contextDB.Proyecto_Workers.Update(OldWorker).Entity;
            await _contextDB.SaveChangesAsync();
            return result;
        }
        /// <summary>
        /// Creates a new worker
        /// </summary>
        /// <param name="model">DTO</param>
        /// <returns>new Worker or null if couldnt be created</returns>
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
                var workerStatus = await _enums.GetWorkerStatusAsync(model.WorkerStatusId);
                Worker newWorker = new()
                {
                    Name = model.FullName,
                    AppUser = user,
                    StartDate = model.StartDate,
                    WorkerStatus = workerStatus
                };
                try
                {
                    var worker = await _contextDB.Proyecto_Workers.AddAsync(newWorker);
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
        /// <summary>
        /// Get list of worker to list item
        /// </summary>
        /// <returns>list of {Email:string,Name:string}</returns>
        public async Task<IEnumerable<WorkersMailsDTO>> GetWorkersMail()
        {
            return await _contextDB.Proyecto_Workers
                .Select(x => new WorkersMailsDTO() { Name = x.Name, Email = x.AppUser.Email })
                .ToListAsync();
        }
    }
}
