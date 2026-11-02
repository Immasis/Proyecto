using EspeciasSarapiqui.Data;
using EspeciasSarapiqui.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EspeciasSarapiqui.Controllers;

[Authorize]
public class CustomersController : Controller
{
    private readonly ApplicationDbContext db;
    public CustomersController(ApplicationDbContext db) => this.db = db;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var customers = await db.Customers
            .Where(x => x.IsActive)
            .OrderBy(x => x.FullName)
            .Select(x => new CustomerListItemViewModel
            {
                Id = x.Id,
                FullName = x.FullName,
                Identification = x.Identification,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address
            })
            .ToListAsync();

        return View(customers);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Customer customer)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        db.Customers.Add(customer);
        await db.SaveChangesAsync();
        return Ok(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Customer input)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer is null) return NotFound();
        customer.FullName = input.FullName; customer.Identification = input.Identification; customer.Email = input.Email;
        customer.Phone = input.Phone; customer.Address = input.Address; customer.IsActive = input.IsActive;
        await db.SaveChangesAsync();
        return Ok(customer);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await db.Customers.FindAsync(id);
        if (customer is null) return NotFound();
        customer.IsActive = false;
        await db.SaveChangesAsync();
        return Ok();
    }
}
