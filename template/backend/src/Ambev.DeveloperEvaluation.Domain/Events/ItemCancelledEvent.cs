using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

///  when a sale item is cancelled
public class ItemCancelledEvent
{

    public Guid SaleId { get; }


    public SaleItem Item { get; }


    public DateTime Timestamp { get; }


    public string? CancellationReason { get; }


    /// <param name="saleId">The sale ID</param>
    /// <param name="item">The cancelled item</param>
    /// <param name="cancellationReason">Reason for cancellation</param>
    public ItemCancelledEvent(Guid saleId, SaleItem item, string? cancellationReason = null)
    {
        SaleId = saleId;
        Item = item;
        CancellationReason = cancellationReason;
        Timestamp = DateTime.UtcNow;
    }
}
