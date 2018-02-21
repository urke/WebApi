using Microsoft.EntityFrameworkCore;
using SIENN.Models;
using System;
using System.Collections.Generic;
using System.Text;


namespace SIENN.DAL
{
    public class SIENNDbContext : DbContext
    {
        public SIENNDbContext(DbContextOptions<SIENNDbContext> options) : base(options)
        {
        }
 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            //Many to many (product-category relationship)
            modelBuilder.Entity<ProductCategory>()
                        .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                        .HasOne<Product>(pc => pc.Product)
                        .WithMany(p => p.ProductCategories)
                        .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                        .HasOne<Category>(pc => pc.Category)
                        .WithMany(c => c.ProductCategories)
                        .HasForeignKey(pc => pc.CategoryId);

            //One to many (product-type relationship)
            modelBuilder.Entity<Models.Type>()
                        .HasMany(t => t.Products)
                        .WithOne(p => p.Type);


            //One to many (product-unit relationship)
            modelBuilder.Entity<Unit>()
                        .HasMany<Product>(u => u.Products)
                        .WithOne(p => p.Unit)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Models.Type> Types { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

    }
}
