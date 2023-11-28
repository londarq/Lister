using Lister.Services.Abstractions;
using Lister.Services.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [Authorize]
    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserById(int userId)
    {
        if (userId <= 0)
        {
            return UnprocessableEntity("Invalid id");
        }

        var result = await _userService.GetUserAsync(userId);

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }

}
