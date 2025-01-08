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
        public static async Task LoadManufacturers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.Manufacturers.Any()) return;

            var json = File.ReadAllText("Data/json/manufacturers.json");
            var manufacturers = JsonSerializer.Deserialize<List<Manufacturer>>(json, options);

            if (manufacturers is not null && manufacturers.Count > 0)
            {
                await context.Manufacturers.AddRangeAsync(manufacturers);
                await context.SaveChangesAsync();
            }
        }
        public static async Task LoadProductmanufacturers(DataContext context)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (context.ProductManufacturers.Any()) return;

            var json = File.ReadAllText("Data/json/productManufacturers.json");
            var productManufacturer = JsonSerializer.Deserialize<List<ProductManufacturer>>(json, options);

            if (productManufacturer is not null && productManufacturer.Count > 0)
            {
                await context.ProductManufacturers.AddRangeAsync(productManufacturer);
                await context.SaveChangesAsync();
            }
        }
    }
}