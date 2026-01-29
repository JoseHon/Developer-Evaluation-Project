using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;


public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateSaleHandler> _logger;

    /// <summary>
    /// Initializes a new instance of CreateSaleHandler
    /// </summary>
    /// <param name="saleRepository">The sale repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="logger">The logger instance</param>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        ILogger<CreateSaleHandler> logger)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <summary>
    /// Handles the CreateSaleCommand request
    /// </summary>
    /// <param name="command">The CreateSale command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // Now i can verifty if this number already exists
        var existingSale = await _saleRepository.GetBySaleNumberAsync(command.SaleNumber, cancellationToken);
        if (existingSale != null)
            throw new InvalidOperationException($"Sale with number {command.SaleNumber} already exists");

        // Create sale entity
        var sale = new Sale
        {
            SaleNumber = command.SaleNumber,
            SaleDate = command.SaleDate,
            CustomerId = command.CustomerId,
            CustomerName = command.CustomerName,
            BranchId = command.BranchId,
            BranchName = command.BranchName
        };

        // Add items to sale
        foreach (var itemDto in command.Items)
        {
            var item = new SaleItem
            {
                ProductId = itemDto.ProductId,
                ProductTitle = itemDto.ProductTitle,
                Quantity = itemDto.Quantity,
                UnitPrice = itemDto.UnitPrice
            };

            sale.AddItem(item);
        }

        // Validate the complete sale
        sale.Validate();

        // Save to database
        var createdSale = await _saleRepository.CreateAsync(sale, cancellationToken);

        // Publish SaleCreated event (log)
        var saleCreatedEvent = new SaleCreatedEvent(createdSale);
        _logger.LogInformation(
            "SaleCreated Event: Sale {SaleId} - Number {SaleNumber} created at {Timestamp}. Total Amount: {TotalAmount:C}",
            saleCreatedEvent.Sale.Id,
            saleCreatedEvent.Sale.SaleNumber,
            saleCreatedEvent.Timestamp,
            saleCreatedEvent.Sale.TotalAmount);

        // Return result
        return new CreateSaleResult
        {
            Id = createdSale.Id,
            SaleNumber = createdSale.SaleNumber,
            TotalAmount = createdSale.TotalAmount
        };
    }
}
