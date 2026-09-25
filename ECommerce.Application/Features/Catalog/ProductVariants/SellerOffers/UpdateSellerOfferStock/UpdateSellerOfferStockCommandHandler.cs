using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.UpdateSellerOfferStock;

public sealed class UpdateSellerOfferStockCommandHandler(ISellerOfferRepository sellerOfferRepository,IUnitOfWork unitOfWork) : IRequestHandler<UpdateSellerOfferStockCommand>
{
    public async Task Handle(UpdateSellerOfferStockCommand command, CancellationToken cancellationToken)
    {
        var sellerOffer =
            await sellerOfferRepository.GetByIdForUpdateAsync(command.SellerOfferId, command.RowVersion,
                cancellationToken);
        if (sellerOffer is null)
            throw new NotFoundException($"Seller offer with id '{command.SellerOfferId}' was not found.");

        sellerOffer.UpdateStock(command.Stock);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}