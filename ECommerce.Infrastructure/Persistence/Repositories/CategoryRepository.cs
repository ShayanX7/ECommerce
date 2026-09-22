using ECommerce.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ECommerceDbContext dbContext) : ICategoryRepository
{
    public Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return dbContext.Categories.AnyAsync(x => x.Id == categoryId, cancellationToken);
    }
}