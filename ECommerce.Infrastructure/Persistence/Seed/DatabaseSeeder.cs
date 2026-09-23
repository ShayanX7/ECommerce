using ECommerce.Domain.Entities;
using ECommerce.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.Persistence.Seed;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(ECommerceDbContext dbContext, UserManager<ApplicationUser> userManager,
        CancellationToken cancellationToken = default)
    {
        if (!await dbContext.Brands.AnyAsync(cancellationToken))
        {
            dbContext.Brands.Add(Brand.Create("Apple"));
        }

        if (!await dbContext.Categories.AnyAsync(cancellationToken))
        {
            dbContext.Categories.Add(Category.Create("Mobile Phones"));
        }

        if (await userManager.FindByEmailAsync("seller@test.com") is null)
        {
            var seller = new ApplicationUser
            {
                UserName = "seller@test.com",
                Email = "seller@test.com",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "Seller"
            };

            await userManager.CreateAsync(seller, "Password123!");
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}