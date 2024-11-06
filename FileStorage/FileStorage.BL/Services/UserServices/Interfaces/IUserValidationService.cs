using FileStorage.BL.Models;

namespace FileStorage.BL.Services.UserServices.Interfaces
{
    public interface IUserValidationService
    {
        Task<UserRegistration?> ValidateUserCredentials(UserRegistration userRegistration);
    }
}
