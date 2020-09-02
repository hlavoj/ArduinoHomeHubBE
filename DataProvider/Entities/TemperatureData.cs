using System;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class TemperatureData : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }

        public int? ArduinoId { get; set; }
        public virtual Arduino Arduino { get; set; }

    }
}
