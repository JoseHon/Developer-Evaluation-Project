using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Validator for UpdateSaleCommand
/// </summary>
public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEqual(Guid.Empty)
            .WithMessage("Sale ID is required");

        When(x => x.CustomerId.HasValue, () =>
        {
            RuleFor(x => x.CustomerId!.Value)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer ID cannot be empty");
        });

        When(x => !string.IsNullOrEmpty(x.CustomerName), () =>
        {
            RuleFor(x => x.CustomerName)
                .MaximumLength(200)
                .WithMessage("Customer name cannot exceed 200 characters");
        });

        When(x => x.BranchId.HasValue, () =>
        {
            RuleFor(x => x.BranchId!.Value)
                .NotEqual(Guid.Empty)
                .WithMessage("Branch ID cannot be empty");
        });

        When(x => !string.IsNullOrEmpty(x.BranchName), () =>
        {
            RuleFor(x => x.BranchName)
                .MaximumLength(200)
                .WithMessage("Branch name cannot exceed 200 characters");
        });

        When(x => x.Items != null && x.Items.Any(), () =>
        {
            RuleForEach(x => x.Items)
                .SetValidator(new UpdateSaleItemValidator());
        });
    }
}

/// <summary>
/// Validator for UpdateSaleItemDto
/// </summary>
public class UpdateSaleItemValidator : AbstractValidator<UpdateSaleItemDto>
{
    public UpdateSaleItemValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Product ID is required");

        RuleFor(x => x.ProductTitle)
            .NotEmpty()
            .WithMessage("Product title is required")
            .MaximumLength(200)
            .WithMessage("Product title cannot exceed 200 characters");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero")
            .LessThanOrEqualTo(20)
            .WithMessage("Cannot sell more than 20 identical items");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
}
