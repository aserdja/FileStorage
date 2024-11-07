using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Repositories
{
	public class StoredFileRepository : Repository<StoredFile>, IStoredFileRepository
	{
		private readonly FileStorageDbContext _context;

		public StoredFileRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<ICollection<StoredFile>> GetAllByEmailAsync(string userEmail)
		{
			return await _context.StoredFiles.Where(sf => sf.User.Email.Equals(userEmail)).ToListAsync();
		}
	}
}
