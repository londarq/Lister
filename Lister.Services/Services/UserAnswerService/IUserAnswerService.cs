using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.UserAnswerService;

public interface IUserAnswerService
{
    Task<ExecutionResult<List<UserAnswerApiModel>>> GetUserAnswersAsync(int userId);
}
