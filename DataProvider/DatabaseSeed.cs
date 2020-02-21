using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DataProvider
{
    public interface IDatabaseSeed
    {
        Task Seed();
    }

    public class DatabaseSeed : IDatabaseSeed
    {
        private readonly UserManager<User> _userManager;
        private readonly AdruinoHomeHubDbContext _context;

        public DatabaseSeed(UserManager<User> userManager, AdruinoHomeHubDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task Seed()
        {
            await _context.Database.MigrateAsync().ConfigureAwait(false);

            if (!await _context.Users.AnyAsync())
            {

                string pass = "Test123*";
                var user = new User { UserName = "hlavoj+revoltuser@gmail.com", Email = "hlavoj+revoltuser@gmail.com" , FullName = "User Name"};
                var admin = new User { UserName = "hlavoj+revoltadmin@gmail.com", Email = "hlavoj+revoltadmin@gmail.com" , FullName = "Admin Name"};

                var a = await _userManager.CreateAsync(user, pass);
                var b = await _userManager.CreateAsync(admin, pass);
            }
        }
    }
}
