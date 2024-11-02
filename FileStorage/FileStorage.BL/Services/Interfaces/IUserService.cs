namespace FileStorage.BL.Services.Interfaces
{
	public interface IUserService
	{
		Task<bool> RegisterUserAsync(string name, string login, string email, string password, string passwordConfirmation);
	}
}
