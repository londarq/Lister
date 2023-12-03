using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.TestService;

public interface ITestService
{
    Task<ExecutionResult<List<TestApiModel>>> GetUserTestsAsync(int userId);
}
