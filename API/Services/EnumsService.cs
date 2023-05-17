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
            return await _contextDB.Proyecto_AccountStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<AccountStatus>> GetAccountStatusesAsync()
        {
            return await _contextDB.Proyecto_AccountStatuses.ToListAsync();
        }

        public async Task<CustomerWorkingStatus> GetCustomerWorkingStatusAsync(int id)
        {
            return await _contextDB.Proyecto_WorkingStates.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CustomerWorkingStatus>> GetCustomerWorkingStatusesAsync()
        {
            return await _contextDB.Proyecto_WorkingStates.ToListAsync();
        }

        public async Task<LoanStatus> GetLoanStatusAsync(int id)
        {
            return await _contextDB.Proyecto_LoanStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<LoanStatus>> GetLoanStatusesAsync()
        {
            return await _contextDB.Proyecto_LoanStatuses.ToListAsync();
        }

        public async Task<LoanType> GetLoanTypeAsync(int id)
        {
            return await _contextDB.Proyecto_LoansTypes.FirstOrDefaultAsync(lt=>lt.Id== id);
        }

        public async Task<IEnumerable<LoanType>> GetLoanTypesAsync()
        {
            return await _contextDB.Proyecto_LoansTypes.ToListAsync();
        }

        public async Task<IEnumerable<TranssactionType>> GetTranssactionsTypesAsync()
        {
            return await _contextDB.Proyecto_TranssactionTypes.ToListAsync();
        }

        public async Task<TranssactionType> GetTranssactionTypeAsync(int id)
        {
            return await _contextDB.Proyecto_TranssactionTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<WorkerStatus> GetWorkerStatusAsync(int id)
        {
            return await _contextDB.Proyecto_WorkerStatuses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<WorkerStatus>> GetWorkerStatusesAsync()
        {
            return await _contextDB.Proyecto_WorkerStatuses.ToListAsync();
        }
    }
}
