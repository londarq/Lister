using Lister.Core.Models;

namespace Lister.Services.Abstractions;

public interface IJwtProvider
{
    string Generate(User user);
}
