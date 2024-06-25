using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings
{
    public class ApplicationIdentityContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Brend> Brends { get; set; } = null!;
        public DbSet<ModelKrosovock> ModelKrosovocks { get; set; } = null!;
        public DbSet<Variant> Variants { get; set; } = null!;
        public DbSet<ImageVariant> ImageVariants { get; set; } = null!;
        public DbSet<ShoppingСart> ShoppingСarts { get; set; } = null!;
        public DbSet<CharacteristicProduct> CharacteristicProducts { get; set; } = null!;
        public DbSet<CharacteristicProductValue> CharacteristicProductValues { get; set; } = null!;
        public ApplicationIdentityContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Создаем и добавляем роли при миграции
            //modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser {  });
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" }, 
            new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" },
            new IdentityRole { Id = "3", Name = "Manager", NormalizedName = "MANAGER" });


            modelBuilder.Entity<ShoppingСart>()
                .HasOne(f => f.ApplicationUser)
                .WithMany()
                .HasForeignKey("UserId");
            modelBuilder.Entity<ShoppingСart>()
                .HasOne(f => f.Variant)
                .WithMany(p => p.ShoppingСarts)
                .HasForeignKey(f => f.VariantId);

            modelBuilder.Entity<CharacteristicProduct>()
                .HasOne(f => f.Product)
                .WithMany(p => p.CharacteristicProducts)
                .HasForeignKey(f => f.ProductId);
            modelBuilder.Entity<CharacteristicProductValue>()
                .HasOne(f => f.CharacteristicProduct)
                .WithMany(p => p.CharacteristicProductValues) 
                .HasForeignKey(f => f.CharacteristicProductId);
            modelBuilder.Entity<Image>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Images)
                .HasForeignKey(f => f.ProductId);
            modelBuilder.Entity<ImageVariant>()
                .HasOne(f => f.Variant)
                .WithMany(p => p.ImageVariants)
                .HasForeignKey(f => f.VariantId);
            modelBuilder.Entity<Variant>()
                .HasOne(f => f.Product)
                .WithMany(p => p.Variants)
                .HasForeignKey(f => f.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(f => f.ModelKrosovock)
                .WithMany(p => p.Products)
                .HasForeignKey(f => f.ModelKrosovockId);
            modelBuilder.Entity<ModelKrosovock>()
                .HasOne(f => f.Brend)
                .WithMany(p => p.ModelKrosovocks)
                .HasForeignKey(f => f.BrendID);
        }
    }
}
