using MediatR;

namespace ECommerce.Application.Features.Sellers.SellerRequests.CreateSellerRequest;

public sealed record CreateSellerRequestCommand(Guid UserId, string? Reason) : IRequest<Guid>;