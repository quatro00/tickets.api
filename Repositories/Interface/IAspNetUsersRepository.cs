using tickets.api.Models;
using tickets.api.Models.Domain;
using tickets.api.Models.DTO.Admin.Auth;

namespace tickets.api.Repositories.Interface
{
    public interface IAspNetUsersRepository : IGenericRepository<AspNetUser>
    {
    }
}
