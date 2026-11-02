using System.Security.Claims;
using EspeciasSarapiqui.Data;
using EspeciasSarapiqui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspeciasSarapiqui.Controllers;

[Authorize]
public class SalesController : Controller
{
    private readonly ApplicationDbContext db;
    public SalesController(ApplicationDbContext db) => this.db = db;

    [HttpGet]
    public async Task<IActionResult> Index() => Json(await db.Sales.Include(x => x.Customer).Include(x => x.Details).OrderByDescending(x => x.SaleDate).Take(100).ToListAsync());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateSaleViewModel model)
    {
        if (!ModelState.IsValid || model.Details.Count == 0) return BadRequest(ModelState);
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId is null) return Unauthorized();
        await using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            var productIds = model.Details.Select(x => x.ProductId).Distinct().ToList();
            var products = await db.Products.Where(x => productIds.Contains(x.Id) && x.IsActive).ToDictionaryAsync(x => x.Id);
            if (products.Count != productIds.Count) return BadRequest("Uno o más productos no existen.");
            var sale = new Sale
            {
                InvoiceNumber = $"V-{DateTime.UtcNow:yyyyMMddHHmmssfff}", CustomerId = model.CustomerId,
                PaymentMethod = model.PaymentMethod, UserId = userId, Status = "Completada"
            };
            foreach (var line in model.Details)
            {
                var product = products[line.ProductId];
                if (product.Stock < line.Quantity) return BadRequest($"Existencias insuficientes para {product.Name}.");
                var total = (product.SalePrice * line.Quantity) - line.Discount;
                if (total < 0) return BadRequest("El descuento no puede superar el subtotal.");
                product.Stock -= line.Quantity;
                sale.Details.Add(new SaleDetail { ProductId = product.Id, Quantity = line.Quantity, UnitPrice = product.SalePrice, Discount = line.Discount, LineTotal = total });
                sale.Subtotal += total;
            }
            sale.Tax = Math.Round(sale.Subtotal * 0.13m, 2);
            sale.Total = sale.Subtotal + sale.Tax;
            db.Sales.Add(sale);
            await db.SaveChangesAsync();
            await transaction.CommitAsync();
            return Ok(sale);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
