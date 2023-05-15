using APITrassBank.Context;
using Microsoft.EntityFrameworkCore;

namespace APITrassBank
{
    public interface IResourcesService
    {
        Task<decimal> GetActualInterest();
        Task<decimal> GetResource(int id);
    }
    public class ResourcesService : IResourcesService
    {
        private readonly ContextDB _contextDB;

        public ResourcesService(ContextDB contextDB)
        {
            _contextDB = contextDB;
        }
        public async Task<decimal> GetActualInterest()
        {
            var interest = await _contextDB.Resources.Where(x => x.Name == "Interest").FirstAsync();
            return interest.Value;
        }
        public async Task<decimal> GetResource(int id)
        {
            return await _contextDB.Resources.Where(x=>x.Id== id).Select(x=>x.Value).FirstAsync();
        }

    }
}