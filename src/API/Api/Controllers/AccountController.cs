using Application.Auth.Commands;
using Application.Auth.Models;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
public class AccountController : ApiControllerBase
{
    [HttpPost("GetAccessToken")]
    public async Task<ActionResult<Response<AuthenticateResponse>>> GetAccessToken(LoginUserRequest model)
    {
        var result = await Mediator.Send(new LoginUserCommand(model));
        return Ok(result);
    }

    [HttpPost("GetRefreshToken")]
    public async Task<ActionResult<AuthenticateResponse>> GetRefreshToken(RefreshRequest model)
    {
        var result = await Mediator.Send(new RefreshCommand(model));
        return Ok(result);
    }
}