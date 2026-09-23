using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class SellerOfferRepository(ECommerceDbContext dbContext) : ISellerOfferRepository
{
    public async Task AddAsync(SellerOffer sellerOffer, CancellationToken cancellationToken = default)
    {
        await dbContext.SellerOffers.AddAsync(sellerOffer, cancellationToken);
    }

    public async Task<SellerOffer?> GetByIdAsync(Guid sellerOfferId, CancellationToken cancellationToken = default)
    {
        return await dbContext.SellerOffers.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == sellerOfferId, cancellationToken);
    }
}