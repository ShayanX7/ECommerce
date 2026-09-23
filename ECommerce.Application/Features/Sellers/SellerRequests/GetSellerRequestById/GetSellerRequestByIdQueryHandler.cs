using ECommerce.Application.Abstractions.Persistence;
using MediatR;

namespace ECommerce.Application.Features.Sellers.SellerRequests.GetSellerRequestById;

public sealed class GetSellerRequestByIdQueryHandler(ISellerRequestRepository sellerRequestRepository)
    : IRequestHandler<GetSellerRequestByIdQuery, SellerRequestDto?>
{
    public async Task<SellerRequestDto?> Handle(GetSellerRequestByIdQuery query, CancellationToken cancellationToken)
    {
        var sellerRequest = await sellerRequestRepository.GetByIdAsync(query.Id, cancellationToken);
        if (sellerRequest is null)
            return null;

        return new SellerRequestDto(
            sellerRequest.Id,
            sellerRequest.UserId,
            sellerRequest.Status,
            sellerRequest.Reason,
            sellerRequest.ReviewedByUserId,
            sellerRequest.ReviewedAt);
    }
}