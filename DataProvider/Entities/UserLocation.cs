using System;
using System.Collections.Generic;
using System.Text;

namespace DataProvider.Entities
{
    public class UserLocation
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }
    }
}
