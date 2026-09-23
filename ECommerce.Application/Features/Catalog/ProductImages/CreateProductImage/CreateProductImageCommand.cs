using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductImages.CreateProductImage;

public sealed record CreateProductImageCommand(
    Guid ProductId,
    Guid? ProductVariantId,
    string Url,
    string? AltText,
    int SortOrder,
    bool IsPrimary) : IRequest<Guid>;