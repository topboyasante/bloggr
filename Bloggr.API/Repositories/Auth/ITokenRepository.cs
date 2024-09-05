using Bloggr.API.Models.Domain;

namespace Bloggr.API.Repositories.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}