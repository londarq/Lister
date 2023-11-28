using Lister.Core.Models;
using Lister.Services.Abstractions;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Lister.Services.Authentification;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _jwtOptions = options.Value;
    }

    public string Generate(User user)
    {
        var claims = new Claim[] {
            new(JwtRegisteredClaimNames.UniqueName, user.Nickname)
        };

        var signedCredetials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            null,
            DateTime.UtcNow.AddHours(1),
            signedCredetials);

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
