using Lister.Services.Abstractions;
using Lister.Services.Login;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Route("api/login")]
public class LoginController : ControllerBase
{
    private readonly ILoginRequestHandler _loginRequestHandler;

    public LoginController(ILoginRequestHandler loginRequestHandler)
    {
        _loginRequestHandler = loginRequestHandler;
    }

    [HttpPost]
    public async Task<ActionResult> Login([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Nickname))
        {
            return UnprocessableEntity("Nickname is required");
        }

        var tokenResult = await _loginRequestHandler.HandleAsync(request);

        if (!tokenResult.IsSuccessful)
        {
            return BadRequest(tokenResult.ErrorMessage);
        }
        
        return Ok(tokenResult.Data);
    }
}
