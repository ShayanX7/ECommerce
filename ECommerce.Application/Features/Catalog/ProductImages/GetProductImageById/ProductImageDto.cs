namespace ECommerce.Application.Features.Catalog.ProductImages.GetProductImageById;

public sealed record ProductImageDto(
    Guid Id,
    string Url,
    string? AltText,
    int SortOrder,
    bool IsPrimary,
    Guid? ProductVariantId);