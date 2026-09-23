using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.CreateSellerOffer;

public sealed record CreateSellerOfferCommand(Guid SellerId, Guid ProductVariantId, decimal Price, int Stock)
    : IRequest<Guid>;