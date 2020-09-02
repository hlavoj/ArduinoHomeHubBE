using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class Location : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserLocation> UserLocations { get; set; }
    }
}
