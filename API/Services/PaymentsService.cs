using APITrassBank.Context;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace APITrassBank.Services
{
    public interface IPaymentsService
    {
        Task<IEnumerable<Payment>> GetAll();
        Task<IEnumerable<PaymentResponseDTO>> GetById(string idSelf);
        Task<PaymentResponseDTO> Make(PaymentCreateDTO model, string idSelf = null);
    }
    public class PaymentsService : IPaymentsService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;

        public PaymentsService(ContextDB contextDB, IMapper mapper)
        {
            _contextDB = contextDB;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _contextDB.Proyecto_Payments.Include(x => x.Loan).ToListAsync();
        }

        public async Task<IEnumerable<PaymentResponseDTO>> GetById(string idSelf)
        {
            return await _contextDB.Proyecto_Payments.Include(x => x.Loan).Select(x => _mapper.Map<PaymentResponseDTO>(x)).ToListAsync();
        }

        public async Task<PaymentResponseDTO> Make(PaymentCreateDTO model, string idSelf = null)
        {
            var loan = await _contextDB.Proyecto_Loans.Include(x => x.LoanStatus)
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Where(x => x.Id.ToString() == model.LoanId)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var account = await _contextDB.Proyecto_Accounts
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Where(x => x.Id.ToString() == model.AccountId)
                .FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            if (idSelf is not null)
            {
                if (!loan.Customer.AppUser.Id.Equals(idSelf) || !account.Customer.AppUser.Id.Equals(idSelf))
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            if (loan.LoanStatus.Id != 2)
            {
                throw new ArgumentException("Loan is not in approved status");
            }
            if (loan.RemainingInstallments < model.NumberInstallments)
            {
                throw new ArgumentException($"Loan remaining installments are less than {model.NumberInstallments}");
            }
            var due = (loan.TotalAmmount / loan.TotalInstallments) * model.NumberInstallments;
            if (account.Balance < due)
            {
                throw new ArgumentException("The account does not have enough balance");
            }
            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
            var newPayment = new Payment()
            {
                Ammount = due,
                Date = DateTime.UtcNow,
                Loan = loan
            };
            account.Balance -= due;
            loan.RemainingInstallments -= model.NumberInstallments;
            loan.RemainingAmmount -= due;
            if (loan.RemainingInstallments == 0)
            {
                loan.LoanStatusId = 4;
            }
            await _contextDB.Proyecto_Payments.AddAsync(newPayment);
            await _contextDB.SaveChangesAsync();
            scope.Complete();
            return _mapper.Map<PaymentResponseDTO>(newPayment);
        }
    }
}
