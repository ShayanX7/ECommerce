using FluentValidation;

namespace ECommerce.Application.Features.Catalog.ProductVariants.CreateProductVariant;

public sealed class CreateProductVariantsCommandValidator : AbstractValidator<CreateProductVariantsCommand>
{
    public CreateProductVariantsCommandValidator()
    {
        RuleFor(x => x.ProductId).NotEmpty();
        RuleFor(x => x.SKU).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Name).NotEmpty().MaximumLength(150);
    }
}