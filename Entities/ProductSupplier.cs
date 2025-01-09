namespace bakery.api.Entities;

public class ProductSupplier
{
    public int ProductId { get; set; }
    public int SupplierId { get; set; }
    public double KiloPrice { get; set; }

    public Product Product { get; set; }
    public Supplier Supplier { get; set; }
}