using APITrassBank.Context;
using Entitys.Entity;

namespace APITrassBank
{
    public interface IScoringsService
    {
        Task<ScoringResponseDTO> GetScoring(ScoringCreateDTO model, string id);
    }
    public class ScoringsService : IScoringsService
    {
        private readonly ContextDB _contextDB;
        private readonly IResourcesService _resourcesService;

        public ScoringsService(ContextDB contextDB, IResourcesService resourcesService)
        {
            _contextDB = contextDB;
            _resourcesService = resourcesService;
        }

        public async Task<ScoringResponseDTO> GetScoring(ScoringCreateDTO model, string id)
        {
            var user = _contextDB.Customers.Where(x => x.AppUser.Id == id).FirstOrDefault() ?? throw new ArgumentOutOfRangeException();
            decimal score = 100;
            var anualExtra = await AnualSpend(user, model);
            throw new NotImplementedException();
        }

        private async Task<decimal> AnualSpend(Customer user, ScoringCreateDTO model)
        {
            /*
             * Cambiar paradigma para que sea un porcentaje segun el tipo del prestamo y tus gastos
             * es decir el usuario me da sus gastos y si la resta de sus gastos en su importe mensual 
             * no supera el x porciento de su sueldo se le deja seguir si no no
             * el porcentaje lo sacamos de la db segun el tipo de prestamo
             */
            throw new NotImplementedException();
        }
    }
}