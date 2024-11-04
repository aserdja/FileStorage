using FileStorage.BL.Models;

namespace FileStorage.BL.Services.Interfaces
{
	public interface IUserValidationService
	{
		Task<UserRegistration?> ValidateUserCredentials(UserRegistration userRegistration);
	}
}
