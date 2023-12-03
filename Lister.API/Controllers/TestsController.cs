using Lister.Core.Models;
using Lister.Services.Services.TestService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly ITestService _testService;

    public TestsController(ITestService testService)
    {
        _testService = testService;
    }

    [HttpGet("{userId}")]
    public async Task<ActionResult> GetUserTests(int userId)
    {
        var result = await _testService.GetUserTestsAsync(userId);

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }
}
