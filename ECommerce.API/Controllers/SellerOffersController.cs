using ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.CreateSellerOffer;
using ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.GetSellerOfferById;
using ECommerce.Application.Features.Catalog.ProductVariants.SellerOffers.UpdateSellerOfferPrice;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/seller-offers")]
public class SellerOffersController(ISender sender) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SellerOfferDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var offer = await sender.Send(new GetSellerOfferByIdQuery(id), cancellationToken);
        if (offer is null)
            return NotFound();

        return Ok(offer);
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateSellerOfferCommand command, CancellationToken cancellationToken)
    {
        var offerId = await sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = offerId }, new { id = offerId });
    }

    [HttpPut("price")]
    public async Task<IActionResult> UpdatePrice(UpdateSellerOfferPriceCommand command,
        CancellationToken cancellationToken)
    {
        await sender.Send(command, cancellationToken);
        return NoContent();
    }
}