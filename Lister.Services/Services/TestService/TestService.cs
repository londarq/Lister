using Lister.Database;
using Lister.Services.Models;
using Lister.Services.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.TestService;

public class TestService : ITestService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;

    public TestService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }

    public async Task<ExecutionResult<List<TestApiModel>>> GetUserTestsAsync(int userId)
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var tests = await dbContext.Tests
            .Include(t => t.UserTestHistory.Where(uth => uth.UserID == userId))
            .ToListAsync();
        var models = tests.Select(t => new TestApiModel()
        {
            TestId = t.TestId,
            Name = t.Name,
            Description = t.Description,
            ImageSrc = t.ImageSrc,
            TimeLimitSec = t.TimeLimitSec,
            UserTestHistory = t.UserTestHistory.Select(uth => new TestHistoryApiModel()
            {
                UserID = userId,
                TestID = uth.TestID,
                StartTimestamp = uth.StartTimestamp,
                FinishTimestamp = uth.FinishTimestamp,
            }).ToList()
        });

        return ExecutionResult<List<TestApiModel>>.Successful(models.ToList());
    }
}
