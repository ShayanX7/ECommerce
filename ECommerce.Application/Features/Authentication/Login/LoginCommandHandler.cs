using ECommerce.Application.Abstractions.Identity;
using MediatR;

namespace ECommerce.Application.Features.Authentication.Login;

public sealed class LoginCommandHandler(IIdentityService identityService, IAccessTokenService accessTokenService)
    : IRequestHandler<LoginCommand, LoginResponseDto?>
{
    public async Task<LoginResponseDto?> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await identityService.AuthenticateAsync(command.Email.Trim(), command.Password, cancellationToken);
        if (user is null) return null;

        var accessToken = accessTokenService.Generate(user);

        return new LoginResponseDto(
            accessToken.Token,
            accessToken.ExpiresAtUtc);
    }
}