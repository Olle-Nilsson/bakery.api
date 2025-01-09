namespace bakery.api.Entities;

public class Product
{
    public int ProductId { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public double KiloPrice { get; set; }

    public IList<ProductSupplier> ProductSuppliers { get; set; }
}