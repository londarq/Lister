using Lister.Services.Models;

namespace Lister.Services.Services.UserTestHistoryService;

public interface IUserTestHistoryService
{
    Task<ExecutionResult<List<int>>> GetTestsUserPassed();
}
    