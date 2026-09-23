using ECommerce.Application.Abstractions.Persistence;
using MediatR;

namespace ECommerce.Application.Features.Catalog.Brands.GetBrandById;

public sealed class GetBrandByIdQueryHandler(IBrandRepository brandRepository)
    : IRequestHandler<GetBrandByIdQuery, BrandDto?>
{
    public async Task<BrandDto?> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetByIdAsync(query.Id, cancellationToken);
        if (brand is null) return null;

        return new BrandDto(brand.Id, brand.Name);
    }
}