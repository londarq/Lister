using Lister.Database;
using Lister.Services.Models;
using Lister.Services.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.UserAnswerService;

public class UserAnswerService : IUserAnswerService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;

    public UserAnswerService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }

    public async Task<ExecutionResult<List<UserAnswerApiModel>>> GetUserAnswersAsync(int userId)
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var userAnswers = await dbContext.UserAnswers.ToListAsync();
        var models = userAnswers.Select(ua => new UserAnswerApiModel()
        {
            UserID = ua.UserID,
            SelectedAnswerID = ua.SelectedAnswerID,
        });

        return ExecutionResult<List<UserAnswerApiModel>>.Successful(models.ToList());
    }
}
