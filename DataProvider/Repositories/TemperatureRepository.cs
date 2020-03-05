using System;
using System.Collections.Generic;
using System.Text;
using DataProvider.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace DataProvider.Repositories
{
    public class TemperatureRepository : EntityFrameworkRepository<TemperatureData, int>, IRepository<TemperatureData, int>
    {
        public TemperatureRepository(IUnitOfWorkProvider unitOfWorkProvider, IDateTimeProvider dateTimeProvider) : base(unitOfWorkProvider, dateTimeProvider)
        {
        }


    }
}
