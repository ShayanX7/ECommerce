using ECommerce.Application.Abstractions.Persistence;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductImages.GetProductImageById;

public sealed class GetProductImageByIdQueryHandler(IProductImageRepository productImageRepository)
    : IRequestHandler<GetProductImageByIdQuery, ProductImageDto?>
{
    public async Task<ProductImageDto?> Handle(GetProductImageByIdQuery query, CancellationToken cancellationToken)
    {
        var image = await productImageRepository.GetByIdAsync(query.Id, cancellationToken);
        if (image is null)
            return null;

        return new ProductImageDto(
            image.Id,
            image.Url,
            image.AltText,
            image.SortOrder,
            image.IsPrimary,
            image.ProductVariantId);
    }
}