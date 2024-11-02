using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace FileStorage.DAL.Repositories
{
	public class StoredFileRepository : Repository<StoredFile>, IStoredFileRepository
	{
		private readonly FileStorageDbContext _context;

		public StoredFileRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}
        public async Task CreateAsync(StoredFile storedFile)
        {
            await _context.StoredFiles.AddAsync(storedFile);
        }

    }
}
