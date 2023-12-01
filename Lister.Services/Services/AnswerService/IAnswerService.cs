using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.AnswerService;

public interface IAnswerService
{
    Task<ExecutionResult<List<CorrectAnswerApiModel>>> GetCorrectAnswersAsync();
    Task<ExecutionResult> CreateUserAnswersAsync(List<UserAnswerApiModel> answers);
}
