using Application.Auth.Models;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Auth.Commands;

public record RegisterUserCommand(RegisterUserRequest RegisterUserRequest) : IRequest<IResponse<string>>;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, IResponse<string>>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<IResponse<string>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.CreateUserASync(request.RegisterUserRequest);
    }
}