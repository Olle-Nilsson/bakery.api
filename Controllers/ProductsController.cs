using bakery.api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;


    [HttpGet()]
    public async Task<ActionResult> ListAllProducts()
    {
        var products = await _context.Products
        .Include(ps => ps.ProductSuppliers)
        .Select(p => new
        {
            Id = p.ProductId,
            p.ItemNumber,
            p.Name,
            p.KiloPrice,
            Suppliers = p.ProductSuppliers.Select(s => new
            {
                Id = s.SupplierId,
                s.Supplier.Name,
                s.Supplier.Address,
                s.Supplier.PhoneNumber,
                s.Supplier.Email,
                s.KiloPrice
            })
        })
        .ToListAsync();

        return Ok(new { success = true, data = products });
    }
}
