using artur_gde_krosi_Vue.Server.Models.BdModel;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO.Compression;
using static artur_gde_krosi_Vue.Server.Models.ProductApi;

namespace artur_gde_krosi_Vue.Server.Models.ProjecktSetings
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Image> Images { get; set; } = null!;
        public DbSet<Brend> Brends { get; set; } = null!;
        public DbSet<ModelKrosovock> ModelKrosovocks{ get; set; } = null!;
        public DbSet<Variant> Variants { get; set; } = null!;
        public DbSet<ImageVariant> ImageVariants { get; set; } = null!;
        public DbSet<ShoppingСart> ShoppingСarts { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ShoppingСart>()
            //    .HasOne(ApplicationUser)
            //    .WithMany()
            //    .HasForeignKey("UserId");
            modelBuilder.Entity<ShoppingСart>()
                .HasOne(f => f.Variant)
                .WithMany(p => p.ShoppingСarts)
                .HasForeignKey(f => f.VariantId);
            
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
