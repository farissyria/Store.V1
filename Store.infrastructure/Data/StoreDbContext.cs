using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Store.Core.Entities;

namespace Store.infrastructure.Data
{
    public class StoreDbContext : IdentityDbContext<User>
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            base.OnModelCreating(mb);

            mb.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            mb.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // Seed data
            mb.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics", Description = "Electronic devices", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Id = 2, Name = "Books", Description = "Books and publications", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Category { Id = 3, Name = "Clothing", Description = "Apparel and fashion", IsActive = true, CreatedAt = DateTime.UtcNow }
            );

            mb.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 999.99m, StockQuantity = 10, CategoryId = 1, Description = "High performance laptop", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Product { Id = 2, Name = "Mouse", Price = 29.99m, StockQuantity = 50, CategoryId = 1, Description = "Wireless mouse", IsActive = true, CreatedAt = DateTime.UtcNow },
                new Product { Id = 3, Name = "C# Book", Price = 49.99m, StockQuantity = 30, CategoryId = 2, Description = "Learn C# programming", IsActive = true, CreatedAt = DateTime.UtcNow }
            );
        }
    }
}