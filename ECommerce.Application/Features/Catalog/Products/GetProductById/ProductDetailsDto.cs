using ECommerce.Application.Features.Catalog.Brands.GetBrandById;
using ECommerce.Application.Features.Catalog.Categories.GetCategoryById;
using ECommerce.Application.Features.Catalog.ProductImages.GetProductImageById;
using ECommerce.Application.Features.Catalog.ProductVariants.GetProductVariantById;
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