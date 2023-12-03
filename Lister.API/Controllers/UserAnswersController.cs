using Lister.Core.Models;
using Lister.Services.Services.TestService;
using Lister.Services.Services.UserAnswerService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserAnswersController : ControllerBase
{
    private readonly IUserAnswerService _userAnswerService;

    public UserAnswersController(IUserAnswerService userAnswerService)
    {
        _userAnswerService = userAnswerService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserAnswers(int userId)
    {
        var result = await _userAnswerService.GetUserAnswersAsync(userId);

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }
}
