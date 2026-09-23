using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Catalog.Categories.CreateCategory;

public sealed class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        if (command.ParentCategoryId.HasValue)
        {
            var parentExist = await categoryRepository.ExistsAsync(command.ParentCategoryId.Value, cancellationToken);
            if (!parentExist)
                throw new NotFoundException(
                    $"Parent category with id '{command.ParentCategoryId.Value}' was not found.'");
        }

        var category = Category.Create(command.Name, command.ParentCategoryId);
        await categoryRepository.AddAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}