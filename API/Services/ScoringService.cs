using APITrassBank.Context;
using APITrassBank.Models;
using APITrassBank.Services;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank
{
    public interface IScoringsService
    {
        Task<LoanResponseDTO> ConfirmScore(ScoringCreateDTO model, string idSelf);
        Task<bool> GetScoring(ScoringCreateDTO model, string id);
    }
    public class ScoringsService : IScoringsService
    {
        private readonly ContextDB _contextDB;
        private readonly IEnumsService _enumsService;
        private readonly ILoansService _loansService;
        private readonly IMapper _mapper;
        private readonly IMessagesService _messagesService;

        public ScoringsService(ContextDB contextDB, IEnumsService enumsService, ILoansService loansService, IMapper mapper, IMessagesService messagesService)
        {
            _contextDB = contextDB;
            _enumsService = enumsService;
            _loansService = loansService;
            _mapper = mapper;
            _messagesService = messagesService;
        }

        public async Task<LoanResponseDTO> ConfirmScore(ScoringCreateDTO model, string idSelf)
        {
            var user = _contextDB.Proyecto_Customers.Include(x => x.Worker).ThenInclude(x => x.AppUser).Where(c => c.AppUser.Id == idSelf).FirstOrDefault() ?? throw new Exception("User not valid");
            var loan = await _loansService.CreateLoan(model, user);
            var scoring = new Scoring()
            {
                DateTime = DateTime.Now,
                Salary = user.Income,
                Spens = (decimal)Spends(model.Expenses),
                Loan = loan,
                Deposit = (decimal)model.Deposit
            };
            var newScor = await _contextDB.Proyecto_Scoring.AddAsync(scoring);
            await _contextDB.SaveChangesAsync();
            await _messagesService.Create(idSelf, new MessageCreateDTO() { Title = "New loan pending", Body = $"There is a new loan by {user.FirstName} {user.LastName}, please check it", ReciverUserName = user.Worker.AppUser.UserName });
            return _mapper.Map<LoanResponseDTO>(loan);
        }

        public async Task<bool> GetScoring(ScoringCreateDTO model, string id)
        {
            var user = await _contextDB.Proyecto_Customers.Where(x => x.AppUser.Id == id).FirstOrDefaultAsync() ?? throw new ArgumentOutOfRangeException();
            var percentaje = await _enumsService.GetLoanTypeAsync(model.LoanTypeId) ?? throw new ArgumentOutOfRangeException();
            var salary = user.Income;
            var expenses = Spends(model.Expenses);
            var avalible = (float)salary - expenses + model.Deposit;
            var totalAmmount = model.Ammount + (model.Ammount * (model.TIN_TAE == 1 ? percentaje.TIN : percentaje.TAE) / 100);
            var mensual = totalAmmount / model.TotalInstallments;
            if (avalible < mensual)
            {
                return false;
            }
            var minimalAvalible = avalible - (avalible * percentaje.Percentaje / 100);
            if (minimalAvalible < mensual)
            {
                return false;
            }
            return true;
        }

        private float Spends(IEnumerable<Expenses> expenses)
        {
            var total = 0f;
            foreach (var expense in expenses)
            {
                total += expense.Spend;
            }
            return total;
        }
    }
}