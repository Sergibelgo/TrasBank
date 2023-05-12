using APITrassBank.Context;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface IEnumsService
    {
        Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync();
        Task<AccountStatus> GetAccountStatusAsync(int id);
        Task<IEnumerable<LoanStatus>> GetLoanStatusesAsync();
        Task<LoanStatus> GetLoanStatusAsync(int id);
        Task<IEnumerable<LoanType>> GetLoanTypesAsync();
        Task<LoanType> GetLoanTypeAsync(int id);
        Task<IEnumerable<TranssactionType>> GetTranssactionsTypesAsync();
        Task<TranssactionType> GetTranssactionTypeAsync(int id);
        Task<IEnumerable<WorkerStatus>> GetWorkerStatusesAsync();
        Task<WorkerStatus> GetWorkerStatusAsync(int id);
        Task<CustomerWorkingStatus> GetCustomerWorkingStatusAsync(int id);
        Task<IEnumerable<CustomerWorkingStatus>> GetCustomerWorkingStatusesAsync();
    }
    public class EnumsService : IEnumsService
    {
        private readonly ContextDB _contextDB;

        public EnumsService(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }
        public async Task<AccountStatus> GetAccountStatusAsync(int id)
        {
            return await _contextDB.AccountStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync()
        {
            return await _contextDB.AccountStatuses.ToListAsync();
        }

        public async Task<CustomerWorkingStatus> GetCustomerWorkingStatusAsync(int id)
        {
            return await _contextDB.WorkingStates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CustomerWorkingStatus>> GetCustomerWorkingStatusesAsync()
        {
            return await _contextDB.WorkingStates.ToListAsync();
        }

        public async Task<LoanStatus> GetLoanStatusAsync(int id)
        {
            return await _contextDB.LoanStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LoanStatus>> GetLoanStatusesAsync()
        {
            return await _contextDB.LoanStatuses.ToListAsync();
        }

        public async Task<LoanType> GetLoanTypeAsync(int id)
        {
            return await _contextDB.LoansTypes.FirstOrDefaultAsync(lt=>lt.Id== id);
        }

        public async Task<IEnumerable<LoanType>> GetLoanTypesAsync()
        {
            return await _contextDB.LoansTypes.ToListAsync();
        }

        public async Task<IEnumerable<TranssactionType>> GetTranssactionsTypesAsync()
        {
            return await _contextDB.TranssactionTypes.ToListAsync();
        }

        public async Task<TranssactionType> GetTranssactionTypeAsync(int id)
        {
            return await _contextDB.TranssactionTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WorkerStatus> GetWorkerStatusAsync(int id)
        {
            return await _contextDB.WorkerStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WorkerStatus>> GetWorkerStatusesAsync()
        {
            return await _contextDB.WorkerStatuses.ToListAsync();
        }
    }
}
