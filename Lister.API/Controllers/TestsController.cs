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

    [HttpGet]
    public async Task<ActionResult> GetTests()
    {
        var result = await _testService.GetTestsAsync();

        return result.IsSuccessful ? Ok(result.Data) : NotFound(result.ErrorMessage);
    }

    //[HttpGet("{userId}")]
    //public async Task<ActionResult> GetTestById(int id)
}
