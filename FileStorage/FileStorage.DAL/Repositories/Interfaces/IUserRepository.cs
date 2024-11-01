using FileStorage.DAL.Models;

namespace FileStorage.DAL.Repositories.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> GetByEmailAndPasswordAsync(string email, string password);
	}
}
