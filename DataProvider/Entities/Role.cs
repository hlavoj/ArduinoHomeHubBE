using Microsoft.AspNetCore.Identity;
using Riganti.Utils.Infrastructure.Core;

namespace DataProvider.Entities
{
    public class Role : IdentityRole<int>, IEntity<int>
    {

        //public const string Administrators = "Administrators";
        //public const string Organizers = "Organizers";
        //public static readonly string[] AllRoles = { Administrators, Organizers };

    }
}
