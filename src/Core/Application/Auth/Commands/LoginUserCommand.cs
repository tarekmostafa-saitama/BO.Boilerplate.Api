using Application.Auth.Models;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands;

public record LoginUserCommand(LoginUserRequest LoginUserRequest) : IRequest<IResponse<AuthenticateResponse>>;

internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IResponse<AuthenticateResponse>>
{
    private readonly IIdentityService _identityService;


    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IResponse<AuthenticateResponse>> Handle(LoginUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _identityService.SignInUserASync(request.LoginUserRequest);
    }
}