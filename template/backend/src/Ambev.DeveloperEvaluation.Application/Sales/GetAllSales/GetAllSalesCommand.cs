using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

/// <summary>
/// Command for retrieving all sales
/// </summary>
public class GetAllSalesCommand : IRequest<GetAllSalesResult>
{
}
