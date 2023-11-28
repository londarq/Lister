using System.Security.Cryptography;

namespace Lister.Services.Authentification;

internal static class PasswordHashHandler
{
    internal static void CreateHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        passwordSalt = hmac.Key;
    }

    internal static bool VerifyHash(string passwordToVerify, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordToVerify));

        return computedHash.SequenceEqual(passwordHash);
    }
}
