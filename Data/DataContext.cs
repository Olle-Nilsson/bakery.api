using bakery.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Data;

public class DataContext : DbContext
{
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductManufacturer> ProductManufacturers { get; set; }

    public DataContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductManufacturer>().HasKey(pm => new { pm.ProductId, pm.ManufacturerId });
    }
}
