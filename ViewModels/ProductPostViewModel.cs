namespace bakery.api.ViewModels;

public class ProductPostViewModel
{
    public string ItemNumber { get; set; }
    public string Name { get; set; }
    public double KiloPrice { get; set; }
    public List<ProductSupplierPostViewModel> ProductSuppliers { get; set; }
}
