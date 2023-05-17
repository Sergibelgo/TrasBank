using Entitys.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Context
{
    public class ContextDB : IdentityDbContext, IContextDB
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }
        public DbSet<Customer> Proyecto_Customers { get; set; }
        public DbSet<CustomerWorkingStatus> Proyecto_WorkingStates { get; set; }
        public DbSet<AccountType> Proyecto_AccountTypes { get; set; }
        public DbSet<AccountStatus> Proyecto_AccountStatuses { get; set; }
        public DbSet<Account> Proyecto_Accounts { get; set; }
        public DbSet<Transaction> Proyecto_Transactions { get; set; }
        public DbSet<TranssactionType> Proyecto_TranssactionTypes { get; set; }
        public DbSet<Worker> Proyecto_Workers { get; set; }
        public DbSet<WorkerStatus> Proyecto_WorkerStatuses { get; set; }
        public DbSet<Message> Proyecto_Messages { get; set; }
        public DbSet<Loan> Proyecto_Loans { get; set; }
        public DbSet<LoanType> Proyecto_LoansTypes { get; set; }
        public DbSet<LoanStatus> Proyecto_LoanStatuses { get; set; }
        public DbSet<Scoring> Proyecto_Scoring { get; set; }
        public DbSet<Payment> Proyecto_Payments { get; set; }
        public DbSet<ATM> Proyecto_ATMS { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(x => x.Balance).HasPrecision(30, 2);
            modelBuilder.Entity<Account>().Property(x => x.Interest).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.Ammount).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.RemainingAmmount).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.InterestRate).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.TotalAmmount).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.Deposit).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.Spens).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.Salary).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.Deposit).HasPrecision(30, 2);
            modelBuilder.Entity<Customer>().Property(x => x.Income).HasPrecision(30, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.Ammount).HasPrecision(30, 2);
            modelBuilder.Entity<Payment>().Property(x => x.Ammount).HasPrecision(30, 2);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>()
            //base.OnModelCreating(modelBuilder);
        }
    }
}
