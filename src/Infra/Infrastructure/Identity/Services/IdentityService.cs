using Application.Auth.Interfaces;
using Application.Auth.Models;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Repositories;
using Forbids;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly IAuthenticateService _authenticateService;
    private readonly IForbid _forbid;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
        IForbid forbid, IAuthenticateService authenticateService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _forbid = forbid;
        _authenticateService = authenticateService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IResponse<string>> CreateUserASync(RegisterUserRequest registerUserRequest)
    {
        var user = new ApplicationUser
        {
            Email = registerUserRequest.Email,
            FullName = registerUserRequest.FullName
        };
        var createResult = await _userManager.CreateAsync(user, registerUserRequest.Password);
        return createResult.Succeeded
            ? Response.Success(user.Id)
            : Response.Fail<string>(createResult.Errors.Select(error => error.Description).ToList());
    }

    public async Task<IResponse<AuthenticateResponse>> SignInUserASync(LoginUserRequest loginUserRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginUserRequest.Email);
        if (user == null)
            return Response.Fail<AuthenticateResponse>("Failed login attempt");
        var signInResult =
            await _signInManager.PasswordSignInAsync(user, loginUserRequest.Password, false, false);

        return signInResult.Succeeded
            ? Response.Success(await _authenticateService.Authenticate(user.Id, CancellationToken.None))
            : Response.Fail<AuthenticateResponse>("Failed login attempt");
    }

    public async Task<IResponse<Unit>> SignOutUserASync(string userId)
    {
        await _signInManager.SignOutAsync();
        _unitOfWork.RefreshTokensRepository.RemoveRange(x => x.UserId == userId);
        await _unitOfWork.CommitAsync();
        return Response.Success(Unit.Value);
    }


    public async Task<IResponse<string>> GetUserFullNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response.Fail<string>("user not found with id = " + userId);
        return Response.Success(user.FullName);
    }

    public async Task<IResponse<string>> GetUserEmailAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Response.Fail<string>("user not found with id = " + userId);
        return Response.Success(user.Email);
    }
}