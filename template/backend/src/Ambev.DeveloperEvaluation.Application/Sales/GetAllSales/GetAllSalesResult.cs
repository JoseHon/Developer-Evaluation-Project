using Ambev.DeveloperEvaluation.Application.Sales.GetSale;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Response returned when retrieving all sales
/// </summary>
public class GetAllSalesResult
{
    public List<GetSaleResult> Sales { get; set; } = new();

    public int TotalCount { get; set; }
}
