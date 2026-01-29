using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler for processing UpdateSaleCommand requests
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of UpdateSaleHandler
    /// </summary>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<UpdateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the UpdateSaleCommand request
    /// </summary>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException($"Sale with ID {command.Id} not found");

        // Update customer if provided
        if (command.CustomerId.HasValue)
        {
            sale.CustomerId = command.CustomerId.Value;
            sale.CustomerName = command.CustomerName ?? sale.CustomerName;
        }

        // Update branch if provided
        if (command.BranchId.HasValue)
        {
            sale.BranchId = command.BranchId.Value;
            sale.BranchName = command.BranchName ?? sale.BranchName;
        }

        // Update items if provided
        if (command.Items != null && command.Items.Any())
        {
            // Clear existing items
            sale.Items.Clear();

            // Add new items
            foreach (var itemDto in command.Items)
            {
                var item = new SaleItem
                {
                    ProductId = itemDto.ProductId,
                    ProductTitle = itemDto.ProductTitle,
                    Quantity = itemDto.Quantity,
                    UnitPrice = itemDto.UnitPrice
                };

                if (itemDto.Id.HasValue)
                {
                    item.Id = itemDto.Id.Value;
                }

                sale.AddItem(item);
            }
        }

        sale.UpdatedAt = DateTime.UtcNow;

        // Validate the updated sale
        sale.Validate();

        // Save to database
        var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);

        // Publish SaleModified event (log)
        var saleModifiedEvent = new SaleModifiedEvent(updatedSale, "Sale updated");
        _logger.LogInformation(
            "SaleModified Event: Sale {SaleId} - Number {SaleNumber} modified at {Timestamp}. {Description}",
            saleModifiedEvent.Sale.Id,
            saleModifiedEvent.Sale.SaleNumber,
            saleModifiedEvent.Timestamp,
            saleModifiedEvent.ModificationDescription);

        return new UpdateSaleResult
        {
            Id = updatedSale.Id,
            SaleNumber = updatedSale.SaleNumber,
            TotalAmount = updatedSale.TotalAmount
        };
    }
}
