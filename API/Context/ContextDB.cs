using Entitys.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Context
{
    public class ContextDB : IdentityDbContext, IContextDB
    {
        public ContextDB(DbContextOptions<ContextDB> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerWorkingStatus> WorkingStates { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TranssactionType> TranssactionTypes { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<WorkerStatus> WorkerStatuses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanType> LoansTypes { get; set; }
        public DbSet<LoanStatus> LoanStatuses { get; set; }
        public DbSet<Scoring> Scoring { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().Property(x => x.Balance).HasPrecision(30, 2);
            modelBuilder.Entity<Account>().Property(x => x.Interest).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.Ammount).HasPrecision(30, 2);
            modelBuilder.Entity<Loan>().Property(x => x.RemainingAmmount).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.Deposit).HasPrecision(30, 2);
            modelBuilder.Entity<Scoring>().Property(x => x.FIN).HasPrecision(30, 2);
            modelBuilder.Entity<Customer>().Property(x => x.Income).HasPrecision(30, 2);
            modelBuilder.Entity<Transaction>().Property(x => x.Ammount).HasPrecision(30, 2);
            modelBuilder.Entity<Payment>().Property(x => x.Ammount).HasPrecision(30, 2);
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<User>()
            //base.OnModelCreating(modelBuilder);
        }
    }
}
