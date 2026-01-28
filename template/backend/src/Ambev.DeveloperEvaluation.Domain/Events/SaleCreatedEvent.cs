using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

///  when a new sale is created
public class SaleCreatedEvent
{
    public Sale Sale { get; }

    public DateTime Timestamp { get; }


    /// <param name="sale">The created sale</param>
    public SaleCreatedEvent(Sale sale)
    {
        Sale = sale;
        Timestamp = DateTime.UtcNow; 
    }
}
