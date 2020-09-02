using DataProvider.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataProvider
{
    public class AdruinoHomeHubDbContext : IdentityDbContext<User, Role, int>
    {

        public DbSet<Light> Lights { get; set; }
        public DbSet<TemperatureData> Temperatures { get; set; }

        public AdruinoHomeHubDbContext(DbContextOptions<AdruinoHomeHubDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.ToTable("IdentityUsers");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.ToTable("IdentityUserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.ToTable("IdentityUserLogins");
            });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
            {
                b.ToTable("IdentityUserTokens");
            });

            modelBuilder.Entity<Role>(b =>
            {
                b.ToTable("IdentityRoles");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.ToTable("IdentityRoleClaims");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.ToTable("IdentityUserRoles");
            });

            //modelBuilder.Entity<User>().HasData(new User{UserName = "admin" , Email = "hlavoj@gmail.com", FullName = "admin", PasswordHash = });

            modelBuilder.Entity<UserLocation>()
                .HasKey(t => new { t.UserId, t.LocationId});

            modelBuilder.Entity<UserLocation>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserLocations)
                .HasForeignKey(l => l.UserId);

            modelBuilder.Entity<UserLocation>()
                .HasOne(u => u.Location)
                .WithMany(u => u.UserLocations)
                .HasForeignKey(l => l.LocationId);
        }
    }
}
