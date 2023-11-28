using Lister.Services.Abstractions;
using Lister.Services.Authentification;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly IAuthRequestHandler _authRequestHandler;

    public AuthController(IAuthRequestHandler authRequestHandler)
    {
        _authRequestHandler = authRequestHandler;
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Register([FromBody] AuthRequest request)
    {
        if (string.IsNullOrEmpty(request.Nickname))
        {
            return UnprocessableEntity("Nickname is required");
        }

        if (string.IsNullOrEmpty(request.Password))
        {
            return UnprocessableEntity("Password is required");
        }

        var tokenResult = await _authRequestHandler.HandleRegisterAsync(request);

        if (!tokenResult.IsSuccessful)
        {
            return BadRequest(tokenResult.ErrorMessage);
        }
        
        return Ok(tokenResult.Data);
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody] AuthRequest request)
    {
        if (string.IsNullOrEmpty(request.Nickname))
        {
            return UnprocessableEntity("Nickname is required");
        }

        if (string.IsNullOrEmpty(request.Password))
        {
            return UnprocessableEntity("Password is required");
        }

        var tokenResult = await _authRequestHandler.HandleLoginAsync(request);

        if (!tokenResult.IsSuccessful)
        {
            return BadRequest(tokenResult.ErrorMessage);
        }

        return Ok(tokenResult.Data);
    }
}
