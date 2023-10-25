using Shared.ServiceContracts;

namespace Application.Auth.Interfaces;

/// <summary>
///     Interface for generating refresh token.
/// </summary>
public interface IRefreshTokenService : ITokenService, IScopedService
{
}