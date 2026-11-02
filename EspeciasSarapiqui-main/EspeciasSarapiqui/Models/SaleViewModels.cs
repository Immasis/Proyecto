using System.ComponentModel.DataAnnotations;

namespace EspeciasSarapiqui.Models;

public class CreateSaleViewModel
{
    public int? CustomerId { get; set; }
    [Required, MaxLength(30)] public string PaymentMethod { get; set; } = "Efectivo";
    [MinLength(1)] public List<CreateSaleLineViewModel> Details { get; set; } = new();
}

public class CreateSaleLineViewModel
{
    public int ProductId { get; set; }
    [Range(typeof(decimal), "0.001", "999999999")] public decimal Quantity { get; set; }
    [Range(typeof(decimal), "0", "999999999")] public decimal Discount { get; set; }
}
