using APITrassBank.Context;
using AutoMapper;
using Entitys.Entity;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank.Services
{
    public interface IPaymentsService
    {
        Task<IEnumerable<Payment>> GetAll();
        Task<IEnumerable<PaymentResponseDTO>> GetById(string idSelf);
        Task<PaymentResponseDTO> Make(PaymentCreateDTO model, string idSelf = null);
    }
    public class PaymentsService:IPaymentsService
    {
        private readonly IContextDB _contextDB;
        private readonly IMapper _mapper;

        public PaymentsService(IContextDB contextDB,IMapper mapper)
        {
            _contextDB = contextDB;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Payment>> GetAll()
        {
            return await _contextDB.Payments.Include(x=>x.Loan).ToListAsync();
        }

        public async Task<IEnumerable<PaymentResponseDTO>> GetById(string idSelf)
        {
            return await _contextDB.Payments.Include(x => x.Loan).Select(x => _mapper.Map<PaymentResponseDTO>(x)).ToListAsync();
        }

        public Task<PaymentResponseDTO> Make(PaymentCreateDTO model,string idSelf=null)
        {
            throw new NotImplementedException();
        }
    }
}
