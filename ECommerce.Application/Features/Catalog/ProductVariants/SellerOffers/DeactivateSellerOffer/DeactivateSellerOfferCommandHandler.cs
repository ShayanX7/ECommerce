using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.DeactivateSellerOffer;

public sealed class DeactivateSellerOfferCommandHandler(
    ISellerOfferRepository sellerOfferRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeactivateSellerOfferCommand>
{
    public async Task Handle(DeactivateSellerOfferCommand command, CancellationToken cancellationToken)
    {
        var sellerOffer =
            await sellerOfferRepository.GetByIdForUpdateAsync(command.SellerOfferId, command.RowVersion,
                cancellationToken);
        if (sellerOffer is null)
            throw new NotFoundException($"Seller offer with id '{command.SellerOfferId}' was not found.");

        sellerOffer.Deactivate();
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}