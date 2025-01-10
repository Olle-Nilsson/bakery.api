using bakery.api.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace bakery.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SuppliersController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet("{id}")]
    public async Task<ActionResult> FindSupplier(int id)
    {

        var supplier = await _context.Suppliers
        .Where(s => s.SupplierId == id)
        .Include(ps => ps.ProductSupplier)
        .Select(s => new
        {
            s.Name,
            s.Address,
            s.PhoneNumber,
            s.Email,
            Products = s.ProductSupplier.Select(p => new
            {
                p.Product.ProductId,
                p.Product.ItemNumber,
                p.Product.Name,
                p.KiloPrice
            })

        })
        .FirstOrDefaultAsync();

        if (supplier is null)
            return NotFound(new { success = false, message = $"No supplier with id: {id} was found in our database" });
        else
            return Ok(new { success = true, data = supplier });
    }
}
