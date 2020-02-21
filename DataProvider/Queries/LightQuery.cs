using System.Linq;
using DataProvider.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace DataProvider.Queries
{
    public class LightQuery : EntityFrameworkQuery<Light>
    {
        public LightQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<Light> GetQueryable()
        {
            return Context.Set<Light>().Select(m => m);
        }
    }
}
