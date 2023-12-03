using Lister.Services.Services.QuestionService;
using Lister.Services.Services.TestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class QuestionsController : ControllerBase
{
    private readonly IQuestionService _questionService;

    public QuestionsController(IQuestionService questionService)
    {
        _questionService = questionService;
    }

    [HttpGet("{testId}")]
    public async Task<ActionResult> GetQuestionsByTestId(int testId)
    {
        if (testId < 1) 
        {
            return BadRequest("Invalid testId");
        }

        var result = await _questionService.GetQuestionsByTestIdAsync(testId);

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }
}
