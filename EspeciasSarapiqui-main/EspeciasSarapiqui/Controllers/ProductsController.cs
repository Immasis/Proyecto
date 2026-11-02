using EspeciasSarapiqui.Data;
using EspeciasSarapiqui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspeciasSarapiqui.Controllers;

[Authorize]
public class ProductsController : Controller
{
    private readonly ApplicationDbContext db;
    public ProductsController(ApplicationDbContext db) => this.db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await db.Products
            .Include(x => x.Category)
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();

        return View(products);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        db.Products.Add(product);
        await db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product input)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        product.Sku = input.Sku; product.Name = input.Name; product.Description = input.Description;
        product.Unit = input.Unit; product.PurchasePrice = input.PurchasePrice; product.SalePrice = input.SalePrice;
        product.MinimumStock = input.MinimumStock; product.CategoryId = input.CategoryId; product.IsActive = input.IsActive;
        await db.SaveChangesAsync();
        return Ok(product);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await db.Products.FindAsync(id);
        if (product is null) return NotFound();
        product.IsActive = false;
        await db.SaveChangesAsync();
        return Ok();
    }
}
