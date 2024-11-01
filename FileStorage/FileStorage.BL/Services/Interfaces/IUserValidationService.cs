namespace FileStorage.BL.Services.Interfaces
{
	public interface IUserValidationService
	{
		Task<bool> CheckCredentialsUniqueness(string login, string email);
	}
}
