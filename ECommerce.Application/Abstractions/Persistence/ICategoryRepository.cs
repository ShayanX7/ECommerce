namespace ECommerce.Application.Abstractions.Persistence;

public interface ICategoryRepository
{
    Task<bool> ExistsAsync(Guid categoryId, CancellationToken cancellationToken = default);
}