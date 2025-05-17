using Microsoft.AspNetCore.Identity;
using tickets.api.Models;

namespace tickets.api.Repositories.Interface
{
    public interface ITokenRepository
    {
        string CreateJwtToken(ApplicationUser user, List<string> roles);
        string CreateRestoreToken(ApplicationUser user);
    }
}
