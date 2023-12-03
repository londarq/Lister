using Lister.Core.Models;
using Lister.Database;
using Lister.Services.Models;
using Lister.Services.Models.ApiModels;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.HistoryService;

public class HistoryService : IHistoryService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;

    public HistoryService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }

    public async Task<ExecutionResult> CreateTetsHistoryAsync(TestHistoryApiModel testHistory)
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var entity = new UserTestHistory()
        {
            TestID = testHistory.TestID,
            UserID = testHistory.UserID,
            StartTimestamp = testHistory.StartTimestamp,
            FinishTimestamp = testHistory.FinishTimestamp,
            Score = testHistory.Score,
        };

        await dbContext.UserTestsHistory.AddAsync(entity);
        var changedRows = await dbContext.SaveChangesAsync();

        return changedRows > 0
            ? ExecutionResult.Successful()
            : ExecutionResult.Failed("Error while saving test history");
    }

    public async Task<ExecutionResult<TestHistoryApiModel>> GetTestHistoryByTestIdAsync(int testId)
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var testHistory = await dbContext.UserTestsHistory.FirstOrDefaultAsync(uth => uth.TestID == testId);
        var model = new TestHistoryApiModel()
        {
            TestID = testHistory.TestID,
            UserID = testHistory.UserID,
            StartTimestamp = testHistory.StartTimestamp,
            FinishTimestamp = testHistory.FinishTimestamp,
            Score = testHistory.Score,
        };

        return ExecutionResult<TestHistoryApiModel>.Successful(model);
    }
}
