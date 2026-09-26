using ECommerce.Application.Features.Authentication.Login;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(ISender sender) :ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command, CancellationToken cancellationToken)
    {
        var result = await sender.Send(command, cancellationToken);
        if (result is null)
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        
        return Ok(result);
    }
}