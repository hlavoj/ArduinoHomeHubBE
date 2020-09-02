using System;
using DataProvider.DTOs;
using DataProvider.Entities;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Queries.Interfaces
{
    public interface ITemperatureQuery :IQuery<TemperatureData>
    {
        int ArduinoId { get; set; }
        DateTime? From { get; set; }
        DateTime? To { get; set; }
        SamplingInterval? Interval { get; set; }
    }
}