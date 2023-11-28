using Lister.Services.Login;
using Lister.Services.Models;

namespace Lister.Services.Abstractions;
public interface ILoginRequestHandler
{
    Task<ExecutionResult<string>> HandleAsync(LoginRequest request);
}