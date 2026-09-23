using ECommerce.Application.Features.Catalog.Products.GetProductById;
using ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.GetSellerOfferById;
using ECommerce.Domain.Enums;

namespace ECommerce.Application.Features.Catalog.ProductVariants.GetProductVariantById;

public sealed record ProductVariantDto(
    Guid Id,
    Guid ProductId,
    string SKU,
    string? Name,
    ProductVariantStatus Status,
    IReadOnlyList<SellerOfferDto> Offers);