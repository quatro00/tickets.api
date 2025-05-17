using tickets.api.Models;
using tickets.api.Models.DTO.Admin.Auth;

namespace tickets.api.Repositories.Interface
{
    public interface IAspNetUsersRepository
    {
        Task<ResponseModel> GetUserById(Guid id);
        Task<ResponseModel> ForgotPassword(ForgotPasswordRequestDto model, string token);
    }
}
