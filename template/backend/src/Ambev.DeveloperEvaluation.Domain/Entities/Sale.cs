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


    public SaleStatus Status { get; set; }


    public List<SaleItem> Items { get; set; } = new List<SaleItem>();

 
    public DateTime CreatedAt { get; set; }

 
    public DateTime? UpdatedAt { get; set; }


    public decimal TotalAmount => Items?.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount) ?? 0;


    public bool IsCancelled => Status == SaleStatus.Cancelled;


    public Sale()
    {
        CreatedAt = DateTime.UtcNow;
        SaleDate = DateTime.UtcNow;
        Status = SaleStatus.Active;
        Items = new List<SaleItem>();
    }


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


    public void Cancel()
    {
        Status = SaleStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }


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
