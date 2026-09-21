using ECommerce.Domain.Entities;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Catalog.Products.GetProductById;

public sealed record ProductDetailsDto(
    Guid Id,
    string Name,
    string Description,
    ProductStatus Status,
    BrandDto Brand,
    CategoryDto Category,
    IReadOnlyList<ProductVariantDto> Variants,
    IReadOnlyList<ProductImageDto> Images);

public sealed record BrandDto(
    Guid Id,
    string Name);

public sealed record CategoryDto(
    Guid Id,
    string Name);
    
public sealed record ProductVariantDto(
    Guid Id,
    string SKU,
    string? Name,
    ProductVariantStatus Status,
    IReadOnlyList<SellerOfferDto> Offers);

public sealed record ProductImageDto(
    Guid Id,
    string Url,
    string? AltText,
    int SortOrder,
    bool IsPrimary,
    Guid? ProductVariantId);
    
public sealed record SellerOfferDto(
    Guid Id,
    Guid SellerId,
    decimal Price,
    int Stock,
    SellerOfferStatus Status);