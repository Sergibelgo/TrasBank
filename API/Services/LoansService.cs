using APITrassBank.Context;
using APITrassBank.Models;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface ILoansService
    {
        Task AproveLoan(string id, string idSelf);
        Task<Loan> CreateLoan(LoanCreateWorkerDTO model);
        Task<Loan> CreateLoan(ScoringCreateDTO model, Customer user);
        Task DenyLoan(string id);
        Task<IEnumerable<LoanResponseDTO>> GetAll();
        Task<LoanResponseDTO> GetById(string id);
        Task<IEnumerable<LoanResponseDTO>> GetByUserId(string idSelf);
        Task<IEnumerable<LoanApprovedResponseDTO>> GetByUserIdApproved(string idSelf);
        Task<IEnumerable<LoanResponseDTO>> GetLoansToBe(string idSelf);
    }
    public class LoansService : ILoansService
    {
        private readonly ContextDB _contextDB;
        private readonly IMapper _mapper;
        private readonly IEnumsService _enums;
        private readonly ITransactionsService _transactionsService;

        public LoansService(ContextDB contextDB, IMapper mapper, IEnumsService enums, ITransactionsService transactionsService)
        {
            _contextDB = contextDB;
            _mapper = mapper;
            _enums = enums;
            _transactionsService = transactionsService;
        }
        /// <summary>
        /// change the status of a loan to aproved
        /// </summary>
        /// <param name="id">Id of the loan</param>
        /// <param name="idSelf">Id of app user</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If loan or user not found</exception>
        /// <exception cref="ArgumentException">If loan is not on waiting status</exception>
        public async Task AproveLoan(string id, string idSelf)
        {
            var loan = await _contextDB.Proyecto_Loans.Include(x => x.Customer).Where(x => x.Id.ToString() == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var user = await _contextDB.Users.Where(x => x.Id == idSelf).FirstOrDefaultAsync() ?? throw new ArgumentException();
            var account = await _contextDB.Proyecto_Accounts.Where(x => x.Customer.Id == loan.Customer.Id).FirstOrDefaultAsync() ?? throw new ArgumentException();
            if (loan.LoanStatusId != 1)
            {
                throw new ArgumentException();
            }

            await _transactionsService.AddorRemoveMoney(loan.Ammount, idSelf, account.Id.ToString(),"Loan approved");
            loan.LoanStatusId = 2;
            await _contextDB.SaveChangesAsync();
        }
        /// <summary>
        /// Creates a new loan
        /// </summary>
        /// <param name="model">DTO with loan information</param>
        /// <returns>New loan</returns>
        /// <exception cref="Exception">If user not found</exception>
        public async Task<Loan> CreateLoan(LoanCreateWorkerDTO model)
        {
            var user = _contextDB.Proyecto_Customers.Where(c => c.AppUser.Id == model.CustomerId).FirstOrDefault() ?? throw new Exception("User not valid");
            var newLoan = new Loan()
            {
                InterestRate = (decimal)(model.TIN_TAE == 1 ? (await _enums.GetLoanTypeAsync(model.LoanTypeId)).TIN : (await _enums.GetLoanTypeAsync(model.LoanTypeId)).TAE),
                Ammount = (decimal)model.Ammount,
                LoanName = model.LoanName ?? $"Loan_{model.Ammount}",
                StartDate = DateTime.Now,
                LoanStatusId = 1,
                LoanTypeId = model.LoanTypeId,
                TotalInstallments = model.TotalInstallments,
                RemainingInstallments = model.TotalInstallments,
                Customer = user
            };
            newLoan.EndDate = newLoan.StartDate.AddMonths(model.TotalInstallments);
            newLoan.TotalAmmount = newLoan.Ammount + newLoan.Ammount * newLoan.InterestRate / 100;
            newLoan.RemainingAmmount = newLoan.TotalAmmount;
            await _contextDB.Proyecto_Loans.AddAsync(newLoan);
            await _contextDB.SaveChangesAsync();
            return newLoan;

        }
        /// <summary>
        /// Creates a loan throug a scoring
        /// </summary>
        /// <param name="model">DTO of scoring</param>
        /// <param name="user">user who aplies</param>
        /// <returns>New loan</returns>
        /// <exception cref="Exception">If loan couldnt be created</exception>
        public async Task<Loan> CreateLoan(ScoringCreateDTO model, Customer user)
        {
            var newLoan = new Loan()
            {
                Ammount = (decimal)model.Ammount,
                StartDate = DateTime.Now,
                LoanName = model.Name ?? $"Loan_{model.Ammount}",
                LoanStatusId = 1,
                LoanTypeId = model.LoanTypeId,
                TotalInstallments = model.TotalInstallments,
                RemainingInstallments = model.TotalInstallments,
                Customer = user
            };
            newLoan.EndDate = newLoan.StartDate.AddMonths(model.TotalInstallments);
            newLoan.InterestRate = (decimal)(model.TIN_TAE == 1 ? (await _enums.GetLoanTypeAsync(model.LoanTypeId)).TIN : (await _enums.GetLoanTypeAsync(model.LoanTypeId)).TAE);
            newLoan.TotalAmmount = newLoan.Ammount + (newLoan.Ammount * newLoan.InterestRate / 100);
            newLoan.RemainingAmmount = newLoan.TotalAmmount;
            await _contextDB.Proyecto_Loans.AddAsync(newLoan);
            try
            {
                await _contextDB.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
            return newLoan;

        }
        /// <summary>
        /// Change loan status to denied
        /// </summary>
        /// <param name="id">Id of loan</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If loan not found or not in waiting status</exception>
        public async Task DenyLoan(string id)
        {
            Loan loan = await _contextDB.Proyecto_Loans.Where(x => x.Id.ToString() == id && x.LoanStatusId == 1).FirstOrDefaultAsync()
                        ?? throw new ArgumentOutOfRangeException();
            loan.LoanStatusId = 3;
            await _contextDB.SaveChangesAsync();
        }
        /// <summary>
        /// Get all loans
        /// </summary>
        /// <returns>List of all loans</returns>
        public async Task<IEnumerable<LoanResponseDTO>> GetAll()
        {
            return await _contextDB.Proyecto_Loans
                .Include(x => x.Customer)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).ToListAsync();
        }
        /// <summary>
        /// Get loan by id
        /// </summary>
        /// <param name="id">Id of loan</param>
        /// <returns>Loan</returns>
        public async Task<LoanResponseDTO> GetById(string id)
        {
            return await _contextDB.Proyecto_Loans
                .Include(x => x.Customer)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Where(x => x.Id.ToString() == id)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).FirstOrDefaultAsync();
        }
        /// <summary>
        /// Get loans by user id
        /// </summary>
        /// <param name="idSelf">Id of user</param>
        /// <returns>List of loans</returns>
        public async Task<IEnumerable<LoanResponseDTO>> GetByUserId(string idSelf)
        {
            return await _contextDB.Proyecto_Loans
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Where(x => x.Customer.AppUser.Id == idSelf || x.Customer.Id.ToString() == idSelf)
                .Select(x => _mapper.Map<LoanResponseDTO>(x)).ToListAsync();
        }
        /// <summary>
        /// Get loans aproved by user id
        /// </summary>
        /// <param name="idSelf">Id user</param>
        /// <returns>List of loans</returns>
        public async Task<IEnumerable<LoanApprovedResponseDTO>> GetByUserIdApproved(string idSelf)
        {
            return await _contextDB.Proyecto_Loans
                .Where(x => x.LoanStatusId == 2)
                .Select(x => new LoanApprovedResponseDTO() { Id = x.Id, Name = x.LoanName })
                .ToListAsync();
        }
        /// <summary>
        /// Get loans in waiting status wich are from customers of Worker id
        /// </summary>
        /// <param name="idSelf">Id of worker</param>
        /// <returns>List of loans</returns>
        public async Task<IEnumerable<LoanResponseDTO>> GetLoansToBe(string idSelf)
        {
            return await _contextDB.Proyecto_Loans
                .Include(x => x.Customer)
                .ThenInclude(x => x.AppUser)
                .Include(x => x.LoanType)
                .Include(x => x.LoanStatus)
                .Where(x=>x.Customer.Worker.AppUser.Id==idSelf)
                .Where(x=>x.LoanStatus.Id==1)
                .Select(x=>_mapper.Map<LoanResponseDTO>(x))
                .ToListAsync();
        }
    }
}
