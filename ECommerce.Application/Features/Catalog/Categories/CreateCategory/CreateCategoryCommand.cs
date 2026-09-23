using MediatR;

namespace ECommerce.Application.Features.Catalog.Categories.CreateCategory;

public sealed record CreateCategoryCommand(string Name,Guid? ParentCategoryId) : IRequest<Guid>;