using Shared.ServiceContracts;

namespace Application.Auth.Interfaces;

/// <summary>
///     Interface for generating access token.
/// </summary>
public interface IAccessTokenService : ITokenService, IScopedService
{
}