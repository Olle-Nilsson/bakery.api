namespace bakery.api.Entities;

public class ProductManufacturer
{
    public int ProductId { get; set; }
    public int ManufacturerId { get; set; }
    public double KiloPrice { get; set; }

    // public Product Product { get; set; }
    // public Manufacturer Manufacturer { get; set; }
}