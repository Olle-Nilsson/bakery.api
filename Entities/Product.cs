namespace bakery.api.Entities;

public class Product
{
    public int Id { get; set; }
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public double KiloPrice { get; set; }

    // public IList<Manufacturer> Manufacturers { get; set; }
}