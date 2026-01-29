using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler for processing CancelSaleCommand requests
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ILogger<CancelSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CancelSaleHandler
    /// </summary>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        ILogger<CancelSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CancelSaleCommand request
    /// </summary>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} not found");

        // Cancel the sale
        sale.Cancel();

        // Save to database
        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publish SaleCancelled event (log)
        var saleCancelledEvent = new SaleCancelledEvent(updatedSale, command.CancellationReason);
        _logger.LogInformation(
            "SaleCancelled Event: Sale ({SaleId}) - Num {SaleNumber} cancelled at {Timestamp}. Reason: {Reason}",
            saleCancelledEvent.Sale.Id,
            saleCancelledEvent.Sale.SaleNumber,
            saleCancelledEvent.Timestamp,
            saleCancelledEvent.CancellationReason ?? "Not specified");

        return new CancelSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            IsCancelled = updatedSale.IsCancelled
        };
    }
}
