using ECommerce.Application.Abstractions.Persistence;
using ECommerce.Application.Exceptions;
using ECommerce.Domain.Entities;
using MediatR;

namespace ECommerce.Application.Features.Sellers.SellerRequests.CreateSellerRequest;

public sealed class CreateSellerRequestCommandHandler(ISellerRequestRepository sellerRequestRepository,IUserRepository userRepository,IUnitOfWork unitOfWork)  :IRequestHandler<CreateSellerRequestCommand,Guid>
{
    public async Task<Guid> Handle(CreateSellerRequestCommand command, CancellationToken cancellationToken)
    {
        var userExists = await userRepository.ExistsAsync(command.UserId, cancellationToken);
        if (!userExists)
            throw new NotFoundException($"User with id '{command.UserId}' was not found.");

        var sellerRequest = SellerRequest.Create(command.UserId, command.Reason);
        await sellerRequestRepository.AddAsync(sellerRequest, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return sellerRequest.Id;
    }
}