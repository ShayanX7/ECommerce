using ECommerce.Domain.Entities;

namespace ECommerce.Application.Abstractions.Persistence;

public interface IBrandRepository
{
    Task<bool> ExistsAsync(Guid brandId, CancellationToken cancellationToken = default);
}