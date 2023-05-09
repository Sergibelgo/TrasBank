using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Context
{
    public interface IContextDB
    {
        DbSet<Customer> Customers { get; set; }
        DbSet<AccountStatus> AccountStatuses { get; set; }
        DbSet<CustomerWorkingStatus> WorkingStates { get; set; }
        DbSet<Transaction> Transactions { get; set; }
        DbSet<TranssactionType> TranssactionTypes { get; set; }
        DbSet<Worker> Workers { get; set; }
        DbSet<WorkerStatus> WorkerStatuses { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<Loan> Loans { get; set; }
        DbSet<LoanType> LoansTypes { get; set; }
        DbSet<LoanStatus> LoanStatuses { get; set; }
        DbSet<Scoring> Scoring { get; set; }
        DbSet<Account> Accounts { get; set; }
        DbSet<Payment> Payments { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
