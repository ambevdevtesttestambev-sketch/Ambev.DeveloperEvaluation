using System;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById;


/// <summary>
/// Response model for GetSales operation
/// </summary>
public class GetSalesResult
{
    /// <summary>
    /// The unique identifier of the sale
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The identifier of the user who made the sale
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// The date and time when the sale occurred
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// The total amount of the sale
    /// </summary>
    public decimal Amount { get; set; }
}
