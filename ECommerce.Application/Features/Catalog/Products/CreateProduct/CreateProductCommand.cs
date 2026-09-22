using MediatR;

namespace ECommerce.Application.Features.Catalog.Products.CreateProduct;

public sealed record CreateProductCommand(string Name, string Description, Guid BrandId, Guid CategoryId) : IRequest<Guid>;