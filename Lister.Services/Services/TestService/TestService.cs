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

    public async Task<ExecutionResult<List<TestApiModel>>> GetTestsAsync()
    {
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var tests = await dbContext.Tests.ToListAsync();
        var models = tests.Select(t => new TestApiModel()
        {
            TestId = t.TestId,
            Name = t.Name,
            Description = t.Description,
            ImageSrc = t.ImageSrc,
            TimeLimitSec = t.TimeLimitSec,
        });

        return ExecutionResult<List<TestApiModel>>.Successful(models.ToList());
    }
}
