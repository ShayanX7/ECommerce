using MediatR;

namespace ECommerce.Application.Features.Sellers.SellerRequests.RejectSellerRequest;

public sealed record RejectSellerRequestCommand(Guid SellerRequestId, Guid AdminUserId) : IRequest;