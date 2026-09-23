using ECommerce.Application.Abstractions.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Repositories;

internal sealed class UserRepository(ECommerceDbContext dbContext) : IUserRepository
{
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.AnyAsync(x => x.Id == userId, cancellationToken);
    }
}