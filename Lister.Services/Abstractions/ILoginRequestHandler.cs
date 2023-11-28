using Lister.Services.Authentification;
using Lister.Services.Models;

namespace Lister.Services.Abstractions;
public interface ILoginRequestHandler
{
    Task<ExecutionResult<string>> HandleAsync(AuthRequest request);
}