using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings
{
    public class ApplicationIdentityContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationIdentityContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Создаем и добавляем роли при миграции
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
            new IdentityRole { Id = "3", Name = "Manager", NormalizedName = "MANAGER" });


        }
    }
}
