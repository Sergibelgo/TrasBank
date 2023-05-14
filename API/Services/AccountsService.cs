namespace APITrassBank.Services
{
    public interface IAccountsService
    {
        Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf);
        Task<IEnumerable<AccountResponseDTO>> GetAll();
        Task<AccountResponseDTO> GetById(string id,string userId=null);
        Task<IEnumerable<AccountResponseDTO>> GetByUserId();
    }
    public class AccountsService : IAccountsService
    {
        public Task<AccountResponseDTO> Create(AccountCreateDTO model, string idSelf)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponseDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponseDTO> GetById(string id, string userId = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponseDTO>> GetByUserId()
        {
            throw new NotImplementedException();
        }
    }
}
