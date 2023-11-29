using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.QuestionService;

public interface IQuestionService
{
    Task<ExecutionResult<List<QuestionApiModel>>> GetQuestionsByTestIdAsync(int testId);
}
