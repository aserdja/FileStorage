namespace FileStorage.BL.Services.Interfaces
{
	public interface IUserValidationService
	{
		Task<bool> CheckCredentialsUniqueness(string login, string email);
		bool CheckCredentialsLength(string name, string login, string email, string password);
	}
}
