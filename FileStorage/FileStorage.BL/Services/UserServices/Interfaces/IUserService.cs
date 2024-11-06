using FileStorage.BL.Models;

namespace FileStorage.BL.Services.UserServices.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUserAsync(UserRegistration newUser);
        Task<bool> LogInUserAsync(UserAuthentication userAuthentication);
    }
}
