using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

/// Represents an item in a sale with product information and proper discount rules.
public class SaleItem : BaseEntity
{

    public Guid SaleId { get; set; }


    public Guid ProductId { get; set; }

    public string ProductTitle { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }


    public decimal Discount { get; set; }

    public bool IsCancelled { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public decimal TotalAmount => Math.Round(Quantity * UnitPrice * (1 - Discount / 100), 2);

    public SaleItem()
    {
        CreatedAt = DateTime.UtcNow;
        IsCancelled = false;
    }

    /// 1. Discount Tiers:
    /// - 4+ items: 10% 
    /// - 10-20 items: 20% 
    /// 
    /// 2. Restrictions:
    /// - Maximum limit: 20 items per product
    /// - No discounts allowed for quantities below 4 items
    public void ApplyDiscountRules()
    {
        if (Quantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items");
        }

        if (Quantity >= 10 && Quantity <= 20)
        {
            Discount = 20;
        }
        else if (Quantity >= 4 && Quantity < 10)
        {
            Discount = 10;
        }
        else
        {
            Discount = 0;
        }

        UpdatedAt = DateTime.UtcNow;
    }


    public void ValidateDiscountRules()
    {
        // Cannot sell more than 20 items
        if (Quantity > 20)
        {
            throw new InvalidOperationException("Cannot sell more than 20 identical items");
        }

        // Below 4 items cannot have discount
        if (Quantity < 4 && Discount > 0)
        {
            throw new InvalidOperationException("Purchases below 4 items cannot have a discount");
        }

        // Validate correct discount for quantity
        if (Quantity >= 10 && Quantity <= 20 && Discount != 20)
        {
            throw new InvalidOperationException("Purchases between 10 and 20 items must have 20% discount");
        }

        if (Quantity >= 4 && Quantity < 10 && Discount != 10)
        {
            throw new InvalidOperationException("Purchases between 4 and 9 items must have 10% discount");
        }
    }

    public void Cancel()
    {
        IsCancelled = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
