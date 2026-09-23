using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.CreateSellerOffer;

public class CreateSellerOfferCommandHandler(
    IUserRepository userRepository,
    IProductVariantRepository productVariantRepository,
    ISellerOfferRepository sellerOfferRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateSellerOfferCommand, Guid>
{
    public async Task<Guid> Handle(CreateSellerOfferCommand command, CancellationToken cancellationToken)
    {
        var sellerExists = await userRepository.ExistsAsync(command.SellerId, cancellationToken);
        if (!sellerExists)
            throw new NotFoundException($"Seller with id '{command.SellerId}' was not found.");

        var variantExist = await productVariantRepository.ExistsAsync(command.ProductVariantId, cancellationToken);
        if (!variantExist)
            throw new NotFoundException($"Product variant with id '{command.ProductVariantId}' was not found.");

        var sellerOffer = SellerOffer.Create(
            command.SellerId,
            command.ProductVariantId,
            command.Price,
            command.Stock);

        await sellerOfferRepository.AddAsync(sellerOffer, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return sellerOffer.Id;
    }
}