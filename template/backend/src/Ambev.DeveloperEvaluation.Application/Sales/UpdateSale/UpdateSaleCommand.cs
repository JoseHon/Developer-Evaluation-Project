using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Command for updating a sale
/// </summary>
public class UpdateSaleCommand : IRequest<UpdateSaleResult>
{
    /// <summary>
    /// Gets or sets the sale ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the customer ID
    /// </summary>
    public Guid? CustomerId { get; set; }

    /// <summary>
    /// Gets or sets the customer name
    /// </summary>
    public string? CustomerName { get; set; }

    /// <summary>
    /// Gets or sets the branch ID
    /// </summary>
    public Guid? BranchId { get; set; }

    /// <summary>
    /// Gets or sets the branch name
    /// </summary>
    public string? BranchName { get; set; }

    /// <summary>
    /// Gets or sets the items to update
    /// </summary>
    public List<UpdateSaleItemDto>? Items { get; set; }
}

/// <summary>
/// DTO for updating a sale item
/// </summary>
public class UpdateSaleItemDto
{
    /// <summary>
    /// Gets or sets the item ID (null for new items)
    /// </summary>
    public Guid? Id { get; set; }

    /// <summary>
    /// Gets or sets the product ID
    /// </summary>
    public Guid ProductId { get; set; }

    /// <summary>
    /// Gets or sets the product title
    /// </summary>
    public string ProductTitle { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the quantity
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the unit price
    /// </summary>
    public decimal UnitPrice { get; set; }
}
