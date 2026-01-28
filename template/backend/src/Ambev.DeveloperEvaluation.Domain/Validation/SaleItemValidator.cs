using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

/// <summary>
/// Validator for SaleItem entity
/// </summary>
public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Product ID is required");

        RuleFor(item => item.ProductTitle)
            .NotEmpty()
            .WithMessage("Product title is required")
            .MaximumLength(200)
            .WithMessage("Product title cannot exceed 200 characters");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");

        RuleFor(item => item.Discount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Discount cannot be negative")
            .LessThanOrEqualTo(100)
            .WithMessage("Discount cannot exceed 100%");

        // Another Layer of validation applying bussiness rule
        // Business rule: Purchases below 4 items cannot have discount
        RuleFor(item => item)
            .Must(item => !(item.Quantity < 4 && item.Discount > 0))
            .WithMessage("Purchases below 4 items cannot have a discount");

        // Business rule: 4-9 items should have 10% discount
        RuleFor(item => item)
            .Must(item => !(item.Quantity >= 4 && item.Quantity < 10 && item.Discount != 10))
            .WithMessage("Purchases between 4 and 9 items must have 10% discount");

        // Business rule: 10-20 items should have 20% discount
        RuleFor(item => item)
            .Must(item => !(item.Quantity >= 10 && item.Quantity <= 20 && item.Discount != 20))
            .WithMessage("Purchases between 10 and 20 items must have 20% discount");
    }
}
