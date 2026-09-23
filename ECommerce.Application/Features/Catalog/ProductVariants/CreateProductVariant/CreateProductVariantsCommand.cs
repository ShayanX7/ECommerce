using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.CreateProductVariant;

public sealed record CreateProductVariantsCommand(Guid ProductId, string SKU, string? Name) : IRequest<Guid>;