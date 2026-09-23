using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class BrandRepository(ECommerceDbContext dbContext) : IBrandRepository
{
    public async Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Brands.AnyAsync(x => x.Id == brandId, cancellationToken);
    }

    public async Task AddAsync(Brand brand, CancellationToken cancellationToken = default)
    {
        await dbContext.Brands.AddAsync(brand, cancellationToken);
    }

    public async Task<Brand?> GetByIdAsync(Guid brandId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Brands.AsNoTracking().FirstOrDefaultAsync(x => x.Id == brandId, cancellationToken);
    }
}