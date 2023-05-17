using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Context
{
    public interface IContextDB
    {
        DbSet<Customer> Proyecto_Customers { get; set; }
        DbSet<AccountStatus> Proyecto_AccountStatuses { get; set; }
        DbSet<CustomerWorkingStatus> Proyecto_WorkingStates { get; set; }
        DbSet<Transaction> Proyecto_Transactions { get; set; }
        DbSet<TranssactionType> Proyecto_TranssactionTypes { get; set; }
        DbSet<Worker> Proyecto_Workers { get; set; }
        DbSet<WorkerStatus> Proyecto_WorkerStatuses { get; set; }
        DbSet<Message> Proyecto_Messages { get; set; }
        DbSet<Loan> Proyecto_Loans { get; set; }
        DbSet<LoanType> Proyecto_LoansTypes { get; set; }
        DbSet<LoanStatus> Proyecto_LoanStatuses { get; set; }
        DbSet<Scoring> Proyecto_Scoring { get; set; }
        DbSet<Account> Proyecto_Accounts { get; set; }
        DbSet<Payment> Proyecto_Payments { get; set; }
        DbSet<AccountType> Proyecto_AccountTypes { get; set; }
        DbSet<ATM> Proyecto_ATMS { get; set; }

        int SaveChanges();
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}
