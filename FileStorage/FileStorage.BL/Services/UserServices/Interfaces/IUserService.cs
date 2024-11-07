using FileStorage.BL.Models;
using FileStorage.DAL.Models;

namespace FileStorage.BL.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegistration newUser);
        Task<bool> LogInUserAsync(UserAuthentication userAuthentication);
    }
}
