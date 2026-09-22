using ECommerce.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class BrandRepository(ECommerceDbContext dbContext) : IBrandRepository
{
    public Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default)
    {
        return dbContext.Brands.AnyAsync(x => x.Id == brandId, cancellationToken);
    }
}