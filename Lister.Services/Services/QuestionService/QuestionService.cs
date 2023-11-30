using Lister.Database;
using Lister.Services.Models;
using Lister.Services.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.QuestionService;

public class QuestionService : IQuestionService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;

    public QuestionService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }
    public async Task<ExecutionResult<List<QuestionApiModel>>> GetQuestionsByTestIdAsync(int testId)
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var questions = await dbContext.Questions
            .Where(q => q.TestID == testId && q.Answers.Any(a => a.QuestionID == q.QuestionID))
            .Include(q => q.Answers)
            .ToListAsync();

        var models = questions.Select(q => new QuestionApiModel()
        {
            QuestionID = q.QuestionID,
            QuestionText = q.QuestionText,
            MediaLink = q.MediaLink,
            Answers = q.Answers.Select(a => new AnswerApiModel()
            {
                AnswerID = a.AnswerID,
                AnswerText = a.AnswerText
            }).ToList()
        });

        return ExecutionResult<List<QuestionApiModel>>.Successful(models.ToList());
    }
}
