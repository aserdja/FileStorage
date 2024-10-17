using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.DAL.Repositories
{
	public class StoredFileRepository : Repository<StoredFile>, IStoredFileRepository
	{
		private readonly FileStorageDbContext _context;

		public StoredFileRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}
	}
}
