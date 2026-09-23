using ECommerce.Application.Features.Sellers.SellerRequests.CreateSellerRequest;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/seller-requests")]
public class SellerRequestsController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSellerRequestCommand command, CancellationToken cancellationToken)
    {
        var sellerId = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = sellerId }, new { id = sellerId });
    }
}