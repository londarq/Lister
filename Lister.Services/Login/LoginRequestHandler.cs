using Lister.Database;
using Lister.Services.Abstractions;
using Lister.Services.Authentification;
using Lister.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Login;

public class LoginRequestHandler : ILoginRequestHandler
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
    private readonly IJwtProvider _jwtProvider;
    public LoginRequestHandler(IDbContextFactory<ApplicationDbContext> pooledFactory, IJwtProvider jwtProvider)
    {
        _pooledFactory = pooledFactory;
        _jwtProvider = jwtProvider;
    }

    public async Task<ExecutionResult<string>> HandleAsync(AuthRequest request)
    {
        if (string.IsNullOrEmpty(request.Nickname) || string.IsNullOrEmpty(request.Nickname))
        {
            return ExecutionResult<string>.Failed("One or more required parameters are missing");
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        //check hash

        var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Nickname == request.Nickname);

        if (user is null)
        {
            return ExecutionResult<string>.Failed("Invalid credentials");
        }

        string token = _jwtProvider.Generate(user);

        if (string.IsNullOrEmpty(token))
        {
            return ExecutionResult<string>.Failed("Token generation error");
        }

        return ExecutionResult<string>.Successful(token);
    }
}
