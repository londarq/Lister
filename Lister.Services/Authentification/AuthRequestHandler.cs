using Lister.Core.Models;
using Lister.Database;
using Lister.Services.Abstractions;
using Lister.Services.Models;
using Microsoft.EntityFrameworkCore;

namespace Lister.Services.Authentification;

public class AuthRequestHandler : IAuthRequestHandler
{
    private readonly IDbContextFactory<ApplicationDbContext> _pooledFactory;
    private readonly IJwtProvider _jwtProvider;

    public AuthRequestHandler(IDbContextFactory<ApplicationDbContext> pooledFactory, IJwtProvider jwtProvider)
    {
        _pooledFactory = pooledFactory;
        _jwtProvider = jwtProvider;
    }

    public async Task<ExecutionResult<string>> HandleRegisterAsync(AuthRequest request)
    {
        if (!IsAuthRequestValid(request, out string validationMessage))
        {
            return ExecutionResult<string>.Failed(validationMessage);
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        PasswordHashHandler.CreateHash(request.Password, out var passwordHash, out var passwordSalt);
        var newUser = new User()
        {
            Nickname = request.Nickname,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        await dbContext.Users.AddAsync(newUser);
        var changedRows = await dbContext.SaveChangesAsync();

        if (changedRows == 0)
        {
            return ExecutionResult<string>.Failed("User creation error");
        }

        string token = _jwtProvider.Generate(newUser);

        if (string.IsNullOrEmpty(token))
        {
            return ExecutionResult<string>.Failed("Token generation error");
        }

        return ExecutionResult<string>.Successful(token);
    }

    public async Task<ExecutionResult<string>> HandleLoginAsync(AuthRequest request)
    {
        if (!IsAuthRequestValid(request, out string validationMessage))
        {
            return ExecutionResult<string>.Failed(validationMessage);
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Nickname == request.Nickname);

        if (user is null)
        {
            return ExecutionResult<string>.Failed("Such user does not exist");
        }

        if (!PasswordHashHandler.VerifyHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return ExecutionResult<string>.Failed("Password is incorrect");
        }

        string token = _jwtProvider.Generate(user);

        if (string.IsNullOrEmpty(token))
        {
            return ExecutionResult<string>.Failed("Token generation error");
        }

        return ExecutionResult<string>.Successful(token);
    }

    private static bool IsAuthRequestValid(AuthRequest request, out string validationMessage)
    {
        validationMessage = string.Empty;
        var isValid = !string.IsNullOrEmpty(request.Nickname) && !string.IsNullOrEmpty(request.Nickname);

        if (isValid)
        {
            validationMessage = "One or more required parameters are missing";
        }

        return isValid;
    }
}
