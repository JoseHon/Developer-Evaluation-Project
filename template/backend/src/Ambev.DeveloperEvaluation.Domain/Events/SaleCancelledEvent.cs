using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

/// Event raised when a sale is cancelled
public class SaleCancelledEvent
{

    public Sale Sale { get; }

    public DateTime Timestamp { get; }
    
    //Pode ser null mesmo
    public string? CancellationReason { get; }


    /// <param name="sale">The cancelled sale</param>
    /// <param name="cancellationReason">Reason for cancellation</param>
    public SaleCancelledEvent(Sale sale, string? cancellationReason = null)
    {
        Sale = sale;
        CancellationReason = cancellationReason;
        Timestamp = DateTime.UtcNow;
    }
}
