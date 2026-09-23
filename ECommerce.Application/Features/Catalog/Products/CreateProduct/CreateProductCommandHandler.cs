using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Catalog.Products.CreateProduct;

public class CreateProductCommandHandler(
    IBrandRepository brandRepository,
    ICategoryRepository categoryRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var brandExists = await brandRepository.ExistsAsync(command.BrandId, cancellationToken);
        if (!brandExists)
            throw new NotFoundException($"Brand with id '{command.BrandId}' was not found.");

        var categoryExists = await categoryRepository.ExistsAsync(command.CategoryId, cancellationToken);
        if (!categoryExists)
            throw new NotFoundException($"Category with id '{command.CategoryId}' was not found.");

        var product = Product.Create(command.Name, command.Description, command.BrandId, command.CategoryId);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return product.Id;
    }
}