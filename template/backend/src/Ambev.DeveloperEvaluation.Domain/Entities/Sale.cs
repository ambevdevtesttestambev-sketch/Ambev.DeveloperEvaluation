namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale
{
    public Guid Id { get; set; }
    public string SaleNumber { get; set; }
    public DateTime SaleDate { get; set; }

    // External Identity + Denormalized Description
    public Guid CustomerId { get; set; }
    public string CustomerName { get; set; }

    public Guid BranchId { get; set; }
    public string BranchName { get; set; }

    public decimal TotalAmount { get; set; }
    public bool IsCancelled { get; set; }

    public List<SaleItem> Items { get; set; } = new();

    // Add this property for user association
    public Guid UserId { get; set; }
}

public class SaleItem
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; }

    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalAmount { get; set; }
}