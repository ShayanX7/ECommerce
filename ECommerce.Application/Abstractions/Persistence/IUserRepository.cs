namespace ECommerce.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
}