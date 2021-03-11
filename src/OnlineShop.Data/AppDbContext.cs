using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Core.Entities;

namespace OnlineShop.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppUser>()
                .HasMany(user => user.Products)
                .WithOne()
                .HasForeignKey(product => product.OwnerId);
            builder.Entity<Product>()
                .Property(product => product.Description)
                .HasMaxLength(Product.DescriptionMaxLength);
            builder.Entity<Order>()
                .HasOne<AppUser>()
                .WithMany()
                .HasForeignKey(order => order.UserId);
            builder.Entity<Order>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(order => order.ProductId);
            builder
                .SeedData();
        }
    }
}