using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Infrastructure.Identity;
using ECommerce.Infrastructure.Persistence;
using ECommerce.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ECommerceDb") ??
                               throw new InvalidOperationException("ECommerceDb is not configured");
        services.AddDbContext<ECommerceDbContext>(options => options.UseNpgsql(connectionString));

        services.AddIdentityCore<ApplicationUser>()
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<ECommerceDbContext>();
        
        services.AddScoped<IProductRepository, ProductRepository>();
        
        return services;
    }
}