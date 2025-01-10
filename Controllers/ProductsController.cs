using bakery.api.Data;
using bakery.api.Entities;
using bakery.api.ViewModels;
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
    [HttpGet("{id}")]
    public async Task<ActionResult> FindProducts(int id)
    {
        var products = await _context.Products
        .Where(p => p.ProductId == id)
        .Include(ps => ps.ProductSuppliers)
        .Select(p => new
        {
            // Id = p.ProductId,
            p.ItemNumber,
            p.Name,
            Suppliers = p.ProductSuppliers.Select(s => new
            {
                // Id = s.SupplierId,
                s.Supplier.Name,
                s.Supplier.Address,
                s.Supplier.PhoneNumber,
                s.Supplier.Email,
                s.KiloPrice
            })
        })
        .FirstOrDefaultAsync();

        if (products is null)
            return NotFound(new { success = false, message = $"No product with id: {id} was found in our database" });
        else
            return Ok(new { success = true, data = products });
    }
    [HttpPost()]
    public async Task<ActionResult> AddProduct(ProductPostViewModel model)
    {
        var exists = await _context.Products.FirstOrDefaultAsync(p => p.ItemNumber == model.ItemNumber);
        if (exists != null) return BadRequest(new { success = false, message = $"Product {model.Name} already exists" });

        var product = new Product
        {
            ItemNumber = model.ItemNumber,
            Name = model.Name,
            KiloPrice = model.KiloPrice
        };

        try
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            foreach (var ps in model.ProductSuppliers)
            {
                var productSupplier = new ProductSupplier
                {
                    ProductId = product.ProductId,
                    SupplierId = ps.SupplierId,
                    KiloPrice = ps.KiloPrice
                };
                await _context.ProductSuppliers.AddAsync(productSupplier);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(FindProducts), new { id = product.ProductId }, new
            {
                product.ProductId,
                product.ItemNumber,
                product.Name,
                product.KiloPrice
            });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdatePrice(int id, [FromQuery] int supplierId, [FromQuery] double price)
    {
        var exists = await _context.ProductSuppliers.FirstOrDefaultAsync(ps => ps.ProductId == id && ps.SupplierId == supplierId);
        if (exists is null) return BadRequest(new { success = false, message = $"Product with id {id} does not exists or is not sold by supllier with id {supplierId}" });

        exists.KiloPrice = price;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}