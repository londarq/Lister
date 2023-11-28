using Lister.Services.Abstractions;
using Lister.Services.Authentification;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Route("api")]
public class AuthController : ControllerBase
{
    private readonly ILoginRequestHandler _loginRequestHandler;

    public AuthController(ILoginRequestHandler loginRequestHandler)
    {
        _loginRequestHandler = loginRequestHandler;
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

        var tokenResult = await _loginRequestHandler.HandleAsync(request);

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

        var tokenResult = await _loginRequestHandler.HandleAsync(request);

        if (!tokenResult.IsSuccessful)
        {
            return BadRequest(tokenResult.ErrorMessage);
        }

        return Ok(tokenResult.Data);
    }
}
