using Lister.Database;
using Lister.Services.ApiModels;
using Lister.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Services.UserService;

public class UserService : IUserService
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
    public UserService(IDbContextFactory<ApplicationDbContext> pooledFactory)
    {
        _pooledFactory = pooledFactory;
    }

    public async Task<ExecutionResult<UserApiModel>> GetUserAsync(int userId)
    {
        if (userId <= 0)
        {
            return ExecutionResult<UserApiModel>.Failed("Invalid id");
        }
        
        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserID == userId);
        var result = user is null
            ? default
            : new UserApiModel() { Nickname = user.Nickname };

        return ExecutionResult<UserApiModel>.Successful(result);
    }
}
