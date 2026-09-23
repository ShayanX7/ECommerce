using FluentValidation;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.CreateSellerOffer;

public sealed class CreateSellerOfferCommandValidator : AbstractValidator<CreateSellerOfferCommand>
{
    public CreateSellerOfferCommandValidator()
    {
        RuleFor(x => x.SellerId).NotEmpty();
        RuleFor(x => x.ProductVariantId).NotEmpty();
        RuleFor(x => x.Price).GreaterThan(0);
        RuleFor(x => x.Stock).GreaterThan(0);
    }
}