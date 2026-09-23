using MediatR;

namespace ECommerce.Application.Features.Catalog.Brands.CreateBrand;

public sealed record CreateBrandCommand(string Name) : IRequest<Guid>;