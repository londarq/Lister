using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.HistoryService;

public interface IHistoryService
{
    Task<ExecutionResult> CreateTetsHistoryAsync(TestHistoryApiModel testHistory);
    Task<ExecutionResult<TestHistoryApiModel>> GetTestHistoryByTestIdAsync(int userId);

}
