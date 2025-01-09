using System.Text.Json;
using bakery.api.Entities;

namespace bakery.api.Data
{
    public static class Seed
    {
        public static async Task LoadProducts(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Products.Any()) return;

            var json = File.ReadAllText("Data/json/products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(json, options);

            if (products is not null && products.Count > 0)
            {
                await context.Products.AddRangeAsync(products);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadSuppliers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Suppliers.Any()) return;

            var json = File.ReadAllText("Data/json/suppliers.json");
            var suppliers = JsonSerializer.Deserialize<List<Supplier>>(json, options);

            if (suppliers is not null && suppliers.Count > 0)
            {
                await context.Suppliers.AddRangeAsync(suppliers);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadProductSuppliers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.ProductSuppliers.Any()) return;

            var json = File.ReadAllText("Data/json/productSuppliers.json");
            var productSuppliers = JsonSerializer.Deserialize<List<ProductSupplier>>(json, options);

            if (productSuppliers is not null && productSuppliers.Count > 0)
            {
                await context.ProductSuppliers.AddRangeAsync(productSuppliers);
                await context.SaveChangesAsync();
            }
        }

    }
}