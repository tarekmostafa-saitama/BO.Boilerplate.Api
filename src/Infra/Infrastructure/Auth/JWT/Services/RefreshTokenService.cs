﻿using Application.Auth.Interfaces;
using Infrastructure.Auth.JWT.Models;

namespace Infrastructure.Auth.JWT.Services;

public class RefreshTokenService : IRefreshTokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly ITokenGenerator _tokenGenerator;

    public RefreshTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings)
    {
        (_tokenGenerator, _jwtSettings) = (tokenGenerator, jwtSettings);
    }

    public async Task<string> Generate(string userId)
    {
        return _tokenGenerator.Generate(new GenerateTokenRequest(
            _jwtSettings.RefreshTokenSecret,
            _jwtSettings.Issuer, _jwtSettings.Audience,
            _jwtSettings.RefreshTokenExpirationMinutes)).Token;
    }
}