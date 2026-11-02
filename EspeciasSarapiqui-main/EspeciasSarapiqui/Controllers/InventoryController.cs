using System.Security.Claims;
using EspeciasSarapiqui.Data;
using EspeciasSarapiqui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspeciasSarapiqui.Controllers;

[Authorize]
public class InventoryController : Controller
{
    private readonly ApplicationDbContext db;
    public InventoryController(ApplicationDbContext db) => this.db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var inventory = await db.Products
            .Where(x => x.IsActive)
            .OrderBy(x => x.Stock)
            .Select(x => new InventoryItemViewModel
            {
                Id = x.Id,
                Sku = x.Sku,
                Name = x.Name,
                Unit = x.Unit,
                Stock = x.Stock,
                MinimumStock = x.MinimumStock,
                PurchasePrice = x.PurchasePrice,
                SalePrice = x.SalePrice,
                Category = x.Category.Name
            })
            .ToListAsync();

        return View(inventory);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Adjust(int productId, decimal quantity, string movementType, string? notes)
    {
        if (quantity <= 0 || (movementType != "Entrada" && movementType != "Salida")) return BadRequest("Movimiento inválido.");
        var product = await db.Products.FindAsync(productId);
        if (product is null) return NotFound();
        var resultingStock = movementType == "Entrada" ? product.Stock + quantity : product.Stock - quantity;
        if (resultingStock < 0) return BadRequest("No hay existencias suficientes.");
        product.Stock = resultingStock;
        db.InventoryMovements.Add(new InventoryMovement { ProductId = productId, Quantity = quantity, MovementType = movementType, Notes = notes, UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)! });
        await db.SaveChangesAsync();
        return Ok(product);
    }
}
