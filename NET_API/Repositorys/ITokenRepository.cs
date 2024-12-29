using Microsoft.AspNetCore.Identity;

namespace NET_API.Repositorys
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
