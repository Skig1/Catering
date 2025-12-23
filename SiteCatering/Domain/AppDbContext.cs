   using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.ValueGeneration;
using SiteCatering.Domain.Entities;

namespace SiteCatering.Domain
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Dish> Dishes { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string adminName = "admin";
            string roleAdminId = Guid.NewGuid().ToString();
            string userAdminId = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleAdminId,
                Name = adminName,
                NormalizedName = adminName.ToUpper()
            });

            builder.Entity<IdentityUser>().HasData(new IdentityUser()
            {
                Id = userAdminId,
                UserName = adminName,
                NormalizedUserName = adminName.ToUpper(),
                Email = "test@yandex.ru",
                NormalizedEmail = "test@yandex.ru",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(new IdentityUser(), adminName),
                SecurityStamp = string.Empty,
                PhoneNumberConfirmed = true
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleAdminId,
                UserId = userAdminId
            });
        }
    }
}
