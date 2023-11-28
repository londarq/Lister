using Lister.Services.Authentification;
using Lister.Services.Models;

namespace Lister.Services.Abstractions;
public interface IAuthRequestHandler
{
    Task<ExecutionResult<string>> HandleRegisterAsync(AuthRequest request);
    Task<ExecutionResult<string>> HandleLoginAsync(AuthRequest request);
}