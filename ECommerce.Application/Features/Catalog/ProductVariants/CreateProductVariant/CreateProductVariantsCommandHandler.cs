using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Catalog.ProductVariants.CreateProductVariant;

public sealed class CreateProductVariantsCommandHandler(
    IProductRepository productRepository,
    IProductVariantRepository productVariantRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductVariantsCommand, Guid>
{
    public async Task<Guid> Handle(CreateProductVariantsCommand command, CancellationToken cancellationToken)
    {
        var productExists = await productRepository.ExistsAsync(command.ProductId, cancellationToken);
        if (!productExists)
            throw new NotFoundException($"Product with id '{command.ProductId}' was not found.");

        var productVariant = ProductVariant.Create(command.ProductId, command.SKU, command.Name);
        await productVariantRepository.AddAsync(productVariant, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return productVariant.Id;
    }
}