using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.DAL.Repositories
{
	public class StoredFileDetailsRepository : Repository<StoredFileDetails>, IStoredFileDetailsRepository
	{
		private readonly FileStorageDbContext _context;

		public StoredFileDetailsRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}



        public async Task CreateAsync(StoredFileDetails storedFileDetails)
        {
        }
    }
}
