using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class User : IdentityUser<int>, IEntity<int>
    {

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateLastLogin { get; set; }

        public bool Enabled { get; set; }

        public ICollection<UserLocation> UserLocations { get; set; }

    }
}
