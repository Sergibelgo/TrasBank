using APITrassBank.Context;
using APITrassBank.Models;

namespace APITrassBank.Services
{
    public interface ILoansService
    {
        Task<IEnumerable<LoanResponseDTO>> GetAll();
        Task<LoanResponseDTO> GetById(string id);
        bool IsValidCreate(LoanCreateDTO model,out string errores);
    }
    public class LoansService : ILoansService
    {
        private readonly ContextDB _contextDB;

        public LoansService(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }
    }
}
