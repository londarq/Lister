using Lister.Core.Models;
using Lister.Database;
using Lister.Services.Models;
using Lister.Services.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.AnswerService;

public class AnswerService : IAnswerService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;

    public AnswerService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }

    public async Task<ExecutionResult<List<CorrectAnswerApiModel>>> GetCorrectAnswersAsync()
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();
        var answers = await dbContext.CorrectAnswers.ToListAsync();
        var models = answers.Select(a => new CorrectAnswerApiModel()
        {
            AnswerID = a.AnswerID
        }).ToList();

        return ExecutionResult<List<CorrectAnswerApiModel>>.Successful(models);
    }

    public async Task<ExecutionResult> CreateUserAnswersAsync(List<UserAnswerApiModel> userAnswers)
    {
        if (userAnswers is null || !userAnswers.Any())
        {
            return ExecutionResult.Failed("Invalid parameters");
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var newEntities = userAnswers.Select(a => new UserAnswer()
        {
            UserID = a.UserID,
            SelectedAnswerID = a.SelectedAnswerID
        });

        await dbContext.UserAnswers.AddRangeAsync(newEntities);
        var changedRows = await dbContext.SaveChangesAsync();

        return changedRows > 0
            ? ExecutionResult.Successful()
            : ExecutionResult.Failed("Error while saving user answers");
    }
}
