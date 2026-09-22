namespace ECommerce.Application.Features.Catalog.Products.CreateProduct;

public sealed record CreateProductRequest(string Name, string Description, Guid BrandId, Guid CategoryId);