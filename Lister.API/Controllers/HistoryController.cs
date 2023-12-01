using Lister.Services.Models.ApiModels;
using Lister.Services.Services.HistoryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lister.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class HistoryController : ControllerBase
{
    private readonly IHistoryService _historyService;

    public HistoryController(IHistoryService historyService)
    {
        _historyService = historyService;
    }

    [HttpPost]
    public async Task<ActionResult> PostTests([FromBody] TestHistoryApiModel testHistory)
    {
        if (testHistory is null)
        {
            return UnprocessableEntity("Parameter is missing");
        }

        var result = await _historyService.CreateTetsHistoryAsync(testHistory);

        return result.IsSuccessful ? Ok() : NotFound(result.ErrorMessage);
    }
}
