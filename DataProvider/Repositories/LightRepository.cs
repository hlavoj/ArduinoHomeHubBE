using DataProvider.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace DataProvider.Repositories
{
    public class LightRepository : EntityFrameworkRepository<Light,int>, IRepository<Light, int>
    {
        public LightRepository(IUnitOfWorkProvider unitOfWorkProvider, IDateTimeProvider dateTimeProvider) : base(unitOfWorkProvider, dateTimeProvider)
        {
        }

        public void TurnOnLight(Light light)
        {
            Context.Set<Light>().Update(light);
        }
    }
}
