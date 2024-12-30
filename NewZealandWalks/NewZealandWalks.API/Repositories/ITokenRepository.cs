using Microsoft.AspNetCore.Identity;

namespace NewZealandWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
