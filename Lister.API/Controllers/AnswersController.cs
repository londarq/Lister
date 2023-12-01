using Lister.Core.Models;
using Lister.Services.Models.ApiModels;
using Lister.Services.Services.AnswerService;
using Lister.Services.Services.TestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class AnswersController : ControllerBase
{
    private readonly IAnswerService _answerService;

    public AnswersController(IAnswerService answerService)
    {
        _answerService = answerService;
    }

    [HttpGet]
    public async Task<ActionResult> GetCorrectAnswers()
    {
        var result = await _answerService.GetCorrectAnswersAsync();

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }

    [HttpPost]
    public async Task<ActionResult> PostUserAnswers([FromBody] List<UserAnswerApiModel> answers)
    {
        if (answers is null || !answers.Any())
        {
            return UnprocessableEntity("Parameter is invalid");
        }

        var result = await _answerService.CreateUserAnswersAsync(answers);

        return result.IsSuccessful ? Ok() : NotFound(result.ErrorMessage);
    }
}
