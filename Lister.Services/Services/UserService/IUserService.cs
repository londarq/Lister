using Lister.Services.ApiModels;
using Lister.Services.Login;
using Lister.Services.Models;

namespace Lister.Services.Services.UserService;

public interface IUserService
{
    Task<ExecutionResult<UserApiModel>> GetUserAsync(int userId);
}
