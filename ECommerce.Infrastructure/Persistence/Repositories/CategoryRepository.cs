using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class CategoryRepository(ECommerceDbContext dbContext) : ICategoryRepository
{
    public async Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.AnyAsync(x => x.Id == categoryId, cancellationToken);
    }

    public async Task AddAsync(Category category, CancellationToken cancellationToken = default)
    {
       await dbContext.Categories.AddAsync(category, cancellationToken);
    }

    public async Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == categoryId, cancellationToken);
    }
}