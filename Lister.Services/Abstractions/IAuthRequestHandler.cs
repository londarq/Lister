using Lister.Services.Authentification;
using Lister.Services.Models;

namespace Lister.Services.Abstractions;
public interface IAuthRequestHandler
{
    Task<ExecutionResult<AuthResponse>> HandleRegisterAsync(AuthRequest request);
    Task<ExecutionResult<AuthResponse>> HandleLoginAsync(AuthRequest request);
}