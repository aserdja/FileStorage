using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> GetByEmailAndPasswordAsync(string email, string password);
		Task<User?> GetByEmailAsync(string email);
		Task<User?> GetByLoginAsync(string login);
	}
}
