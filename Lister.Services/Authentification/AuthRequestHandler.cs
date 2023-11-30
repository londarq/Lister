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

    public async Task<ExecutionResult<AuthResponse>> HandleRegisterAsync(AuthRequest request)
    {
        if (!IsAuthRequestValid(request, out string validationMessage))
        {
            return ExecutionResult<AuthResponse>.Failed(validationMessage);
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        PasswordHashHandler.CreateHash(request.Password, out var passwordHash, out var passwordSalt);
        var newUser = new User()
        {
            Nickname = request.Nickname,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };
        var createdUser = await dbContext.Users.AddAsync(newUser);
        var changedRows = await dbContext.SaveChangesAsync();

        if (changedRows == 0)
        {
            return ExecutionResult<AuthResponse>.Failed("User creation error");
        }

        string token = _jwtProvider.Generate(newUser);

        if (string.IsNullOrEmpty(token))
        {
            return ExecutionResult<AuthResponse>.Failed("Token generation error");
        }

        return ExecutionResult<AuthResponse>.Successful(new AuthResponse(createdUser.Entity.UserID, token));
    }

    public async Task<ExecutionResult<AuthResponse>> HandleLoginAsync(AuthRequest request)
    {
        if (!IsAuthRequestValid(request, out string validationMessage))
        {
            return ExecutionResult<AuthResponse>.Failed(validationMessage);
        }

        using var dbContext = await _pooledFactory.CreateDbContextAsync();

        var user = await dbContext.Users.SingleOrDefaultAsync(x => x.Nickname == request.Nickname);

        if (user is null)
        {
            return ExecutionResult<AuthResponse>.Failed("Such user does not exist");
        }

        if (!PasswordHashHandler.VerifyHash(request.Password, user.PasswordHash, user.PasswordSalt))
        {
            return ExecutionResult<AuthResponse>.Failed("Password is incorrect");
        }

        string token = _jwtProvider.Generate(user);

        if (string.IsNullOrEmpty(token))
        {
            return ExecutionResult<AuthResponse>.Failed("Token generation error");
        }

        return ExecutionResult<AuthResponse>.Successful(new AuthResponse((int)user?.UserID, token));
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
