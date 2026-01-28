using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Events;

///  when a sale is modified
public class SaleModifiedEvent
{

    public Sale Sale { get; }


    public DateTime Timestamp { get; }

    public string ModificationDescription { get; }


    /// <param name="sale">The modified sale</param>
    /// <param name="modificationDescription">Description of the modification</param>
    public SaleModifiedEvent(Sale sale, string modificationDescription)
    {
        Sale = sale;
        ModificationDescription = modificationDescription;
        Timestamp = DateTime.UtcNow;
    }
}
