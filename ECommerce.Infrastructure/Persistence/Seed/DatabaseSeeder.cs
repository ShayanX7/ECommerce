using ECommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ECommerceDbContext dbContext, CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Brands.AnyAsync(cancellationToken))
        {
            dbContext.Brands.Add(Brand.Create("Apple"));
        }

        if (!await dbContext.Categories.AnyAsync(cancellationToken))
        {
            dbContext.Categories.Add(Category.Create("Mobile Phones"));
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}