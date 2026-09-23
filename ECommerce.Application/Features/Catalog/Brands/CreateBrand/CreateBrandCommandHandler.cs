using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Catalog.Brands.CreateBrand;

public sealed class CreateBrandCommandHandler(IBrandRepository brandRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateBrandCommand, Guid>
{
    public async Task<Guid> Handle(CreateBrandCommand command, CancellationToken cancellationToken)
    {
        var brand = Brand.Create(command.Name);
        await brandRepository.AddAsync(brand, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return brand.Id;
    }
}