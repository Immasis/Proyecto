using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EspeciasSarapiqui.Models;

public class Category
{
    public int Id { get; set; }
    [Required, MaxLength(100)] public string Name { get; set; } = string.Empty;
    [MaxLength(300)] public string? Description { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Product> Products { get; set; } = new List<Product>();
}

public class Product
{
    public int Id { get; set; }
    [Required, MaxLength(30)] public string Sku { get; set; } = string.Empty;
    [Required, MaxLength(150)] public string Name { get; set; } = string.Empty;
    [MaxLength(1000)] public string? Description { get; set; }
    [MaxLength(50)] public string Unit { get; set; } = "Unidad";
    [Column(TypeName = "decimal(18,2)")] public decimal PurchasePrice { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal SalePrice { get; set; }
    public decimal MinimumStock { get; set; }
    public decimal Stock { get; set; }
    public bool IsActive { get; set; } = true;
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}

public class Customer
{
    public int Id { get; set; }
    [Required, MaxLength(120)] public string FullName { get; set; } = string.Empty;
    [MaxLength(30)] public string? Identification { get; set; }
    [MaxLength(120), EmailAddress] public string? Email { get; set; }
    [MaxLength(30)] public string? Phone { get; set; }
    [MaxLength(300)] public string? Address { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
}

public class Sale
{
    public int Id { get; set; }
    [MaxLength(30)] public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime SaleDate { get; set; } = DateTime.UtcNow;
    [MaxLength(20)] public string Status { get; set; } = "Completada";
    [MaxLength(30)] public string PaymentMethod { get; set; } = "Efectivo";
    [Column(TypeName = "decimal(18,2)")] public decimal Subtotal { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Tax { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Total { get; set; }
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    [Required] public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<SaleDetail> Details { get; set; } = new List<SaleDetail>();
}

public class SaleDetail
{
    public int Id { get; set; }
    public int SaleId { get; set; }
    public Sale Sale { get; set; } = null!;
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal UnitPrice { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal Discount { get; set; }
    [Column(TypeName = "decimal(18,2)")] public decimal LineTotal { get; set; }
}

public class InventoryMovement
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public DateTime MovementDate { get; set; } = DateTime.UtcNow;
    [MaxLength(20)] public string MovementType { get; set; } = "Entrada";
    public decimal Quantity { get; set; }
    [MaxLength(300)] public string? Notes { get; set; }
    [Required] public string UserId { get; set; } = string.Empty;
    public ApplicationUser User { get; set; } = null!;
}
