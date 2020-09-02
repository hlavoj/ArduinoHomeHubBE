using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class Arduino : IEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string AuthorizationHash { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

    }
}
