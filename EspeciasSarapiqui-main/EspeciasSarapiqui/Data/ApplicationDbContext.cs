using EspeciasSarapiqui.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EspeciasSarapiqui.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Sale> Sales => Set<Sale>();
    public DbSet<SaleDetail> SaleDetails => Set<SaleDetail>();
    public DbSet<InventoryMovement> InventoryMovements => Set<InventoryMovement>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<Product>().HasIndex(x => x.Sku).IsUnique();
        builder.Entity<Sale>().HasIndex(x => x.InvoiceNumber).IsUnique();
        builder.Entity<Customer>().HasIndex(x => x.Identification).IsUnique().HasFilter("[Identification] IS NOT NULL");
        builder.Entity<Product>().Property(x => x.Stock).HasPrecision(18, 3);
        builder.Entity<Product>().Property(x => x.MinimumStock).HasPrecision(18, 3);
        builder.Entity<SaleDetail>().Property(x => x.Quantity).HasPrecision(18, 3);
        builder.Entity<InventoryMovement>().Property(x => x.Quantity).HasPrecision(18, 3);
        builder.Entity<Sale>().HasOne(x => x.Customer).WithMany(x => x.Sales).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.SetNull);
        builder.Entity<Sale>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<InventoryMovement>().HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.Entity<SaleDetail>().HasOne(x => x.Product).WithMany().HasForeignKey(x => x.ProductId).OnDelete(DeleteBehavior.Restrict);
    }
}
