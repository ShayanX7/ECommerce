using FluentValidation;

namespace ECommerce.Application.Features.Catalog.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(250);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.BrandId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        
    }
}