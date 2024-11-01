using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Repositories
{
	public class UserRepository : Repository<User>, IUserRepository
	{
		private readonly FileStorageDbContext _context;

		public UserRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
		{
			var query = _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

			if (query.Result != null)
			{
				return await query;
			}

			return null;
		}

		public Task<User?> GetByEmailAsync(string email)
		{
			return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
		}
	}
}
