using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions.Persistence;

public interface ICategoryRepository
{
    Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default);
    Task AddAsync(Category category, CancellationToken cancellationToken = default);
    Task<Category?> GetByIdAsync(Guid categoryId, CancellationToken cancellationToken = default);
}