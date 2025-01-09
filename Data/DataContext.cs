using bakery.api.Entities;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Data;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductSupplier> ProductSuppliers { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductSupplier>().HasKey(pm => new { pm.ProductId, pm.SupplierId });
    }
}
