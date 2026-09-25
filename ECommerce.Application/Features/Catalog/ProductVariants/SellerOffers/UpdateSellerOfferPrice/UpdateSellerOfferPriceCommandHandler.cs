using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.UpdateSellerOfferPrice;

public sealed class UpdateSellerOfferPriceCommandHandler(
    ISellerOfferRepository sellerOfferRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSellerOfferPriceCommand>
{
    public async Task Handle(UpdateSellerOfferPriceCommand command, CancellationToken cancellationToken)
    {
        var sellerOffer =
            await sellerOfferRepository.GetByIdForUpdateAsync(command.SellerOfferId, command.RowVersion,
                cancellationToken);
        if (sellerOffer is null)
            throw new NotFoundException($"Seller offer with id '{command.SellerOfferId}' was not found.");

        sellerOffer.UpdatePrice(command.Price);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}