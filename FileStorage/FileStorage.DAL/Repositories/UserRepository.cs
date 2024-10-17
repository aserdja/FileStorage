using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.DAL.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly FileStorageDbContext _context;

		public UserRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
