using Lister.Services.Models;

namespace Lister.Services.Services.UserTestHistoryService;

public class UserTestHistoryService : IUserTestHistoryService
{
    public Task<ExecutionResult<List<int>>> GetTestsUserPassed()
    {
        //возвращать либо ид либо объектами теста
        throw new NotImplementedException();
    }
}
