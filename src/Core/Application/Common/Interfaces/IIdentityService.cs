using Application.Auth.Models;
using MediatR;
using Shared.ServiceContracts;

namespace Application.Common.Interfaces;

public interface IIdentityService : IScopedService
{
    Task<IResponse<string>> CreateUserASync(RegisterUserRequest registerUserRequest);
    Task<IResponse<AuthenticateResponse>> SignInUserASync(LoginUserRequest loginUserRequest);
    Task<IResponse<Unit>> SignOutUserASync(string userId);
    Task<IResponse<string>> GetUserFullNameAsync(string userId);
    Task<IResponse<string>> GetUserEmailAsync(string userId);
}