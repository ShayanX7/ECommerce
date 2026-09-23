using ECommerce.Application.Abstractions.Persistence;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.GetSellerOfferById;

public sealed class GetSellerOfferByIdQueryHandler(ISellerOfferRepository sellerOfferRepository)
    : IRequestHandler<GetSellerOfferByIdQuery, SellerOfferDto?>
{
    public async Task<SellerOfferDto?> Handle(GetSellerOfferByIdQuery query, CancellationToken cancellationToken)
    {
        var offer = await sellerOfferRepository.GetByIdAsync(query.Id, cancellationToken);
        if (offer is null)
            return null;

        return new SellerOfferDto(
            offer.Id,
            offer.SellerId,
            offer.Price,
            offer.Stock,
            offer.Status);
    }
}