using Lister.Services.Models;
using Lister.Services.Models.ApiModels;

namespace Lister.Services.Services.UserService;

public interface IUserService
{
    Task<ExecutionResult<UserApiModel>> GetUserAsync(int userId);
}
