using System;
using System.Linq;
using DataProvider.DTOs;
using DataProvider.Entities;
using DataProvider.Queries.Interfaces;
using Microsoft.EntityFrameworkCore;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;

namespace DataProvider.Queries
{
    public class TemperatureQuery : EntityFrameworkQuery<TemperatureData>, ITemperatureQuery
    {

        public int ArduinoId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public SamplingInterval? Interval { get; set; }

        public TemperatureQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }


        protected override IQueryable<TemperatureData> GetQueryable()
        {
            var preFilter = Context.Set<TemperatureData>()
                .Where(u => u.ArduinoId == ArduinoId);

            if (From != null)
                preFilter = preFilter.Where(t => t.DateTime >= From);
            if (To != null)
                preFilter = preFilter.Where(t => t.DateTime <= To);


            IQueryable<IGrouping<DateTime, TemperatureData>> groupBy;
            switch (Interval)
            {
                case SamplingInterval.FifteenMinutes:
                    groupBy = preFilter.GroupBy(x => new DateTime(x.DateTime.Year, x.DateTime.Month, x.DateTime.Day, x.DateTime.Hour, x.DateTime.Minute- x.DateTime.Minute % 15, 0));
                    break;
                case SamplingInterval.Hour:
                    groupBy = preFilter.GroupBy(x => new DateTime(x.DateTime.Year, x.DateTime.Month, x.DateTime.Day, x.DateTime.Hour ,0,0 ));
                    break;
                case SamplingInterval.Day:
                    groupBy = preFilter.GroupBy(x => x.DateTime.Date);
                    break;
                case SamplingInterval.Week:
                    groupBy = preFilter.GroupBy(x => x.DateTime.Date.AddHours(x.DateTime.Hour));
                    break;
                default:
                    return preFilter;
            }

            var result = groupBy.Select(d => new TemperatureData
            {
                DateTime = d.Key,
                Temperature = d.Average(t => t.Temperature),
                Humidity = d.Average(t => t.Humidity),
            });

            return result;
        }
    }
}
