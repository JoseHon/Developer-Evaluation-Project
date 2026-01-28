using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// Represents a sale in the DeveloperStore system.
public class Sale : BaseEntity
{

    public string SaleNumber { get; set; } = string.Empty;


    public DateTime SaleDate { get; set; }

    public Guid CustomerId { get; set; }


    public string CustomerName { get; set; } = string.Empty;


    public Guid BranchId { get; set; }


    public string BranchName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the sale status
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the collection of sale items
    /// </summary>
    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

    /// <summary>
    /// Gets or sets the date and time when the sale was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Gets or sets the date and time of the last update to the sale.
    /// </summary>
    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Calculates the total amount of the sale (sum of all items)
    /// </summary>
    public decimal TotalAmount => Items?.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount) ?? 0;

    /// <summary>
    /// Indicates whether the sale is cancelled
    /// </summary>
    public bool IsCancelled => Status == SaleStatus.Cancelled;

    /// <summary>
    /// Initializes a new instance of the Sale class.
    /// </summary>
    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
        SaleDate = DateTime.UtcNow;
        Status = SaleStatus.Active;
        Items = new List<SaleItem>();
    }

    /// <summary>
    /// Adds an item to the sale and applies discount rules
    /// </summary>
    /// <param name="item">The item to add</param>
    public void AddItem(SaleItem item)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException("Cannot add items to a cancelled sale");
        }

        item.SaleId = Id;
        item.ApplyDiscountRules();
        item.ValidateDiscountRules();
        
        Items.Add(item);
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Updates an existing item in the sale
    /// </summary>
    /// <param name="itemId">The item ID to update</param>
    /// <param name="quantity">New quantity</param>
    /// <param name="unitPrice">New unit price</param>
    public void UpdateItem(Guid itemId, int quantity, decimal unitPrice)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException("Cannot update items in a cancelled sale");
        }

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new InvalidOperationException($"Item with ID {itemId} not found in sale");
        }

        if (item.IsCancelled)
        {
            throw new InvalidOperationException("Cannot update a cancelled item");
        }

        item.Quantity = quantity;
        item.UnitPrice = unitPrice;
        item.ApplyDiscountRules();
        item.ValidateDiscountRules();
        item.UpdatedAt = DateTime.UtcNow;
        
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels a specific item in the sale
    /// </summary>
    /// <param name="itemId">The item ID to cancel</param>
    public void CancelItem(Guid itemId)
    {
        if (IsCancelled)
        {
            throw new InvalidOperationException("Sale is already cancelled");
        }

        var item = Items.FirstOrDefault(i => i.Id == itemId);
        if (item == null)
        {
            throw new InvalidOperationException($"Item with ID {itemId} not found in sale");
        }

        item.Cancel();
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Cancels the entire sale
    /// </summary>
    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Validates the sale
    /// </summary>
    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(SaleNumber))
        {
            throw new InvalidOperationException("Sale number is required");
        }

        if (CustomerId == Guid.Empty)
        {
            throw new InvalidOperationException("Customer ID is required");
        }

        if (BranchId == Guid.Empty)
        {
            throw new InvalidOperationException("Branch ID is required");
        }

        if (!Items.Any())
        {
            throw new InvalidOperationException("Sale must have at least one item");
        }

        // Validate all items
        foreach (var item in Items)
        {
            item.ValidateDiscountRules();
        }
    }
}
