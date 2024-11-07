using FileStorage.DAL.Data;
using FileStorage.DAL.Models;
using FileStorage.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FileStorage.DAL.Repositories
{
	public class StoredFileDetailsRepository : Repository<StoredFileDetails>, IStoredFileDetailsRepository
	{
		private readonly FileStorageDbContext _context;

		public StoredFileDetailsRepository(FileStorageDbContext context) : base(context)
		{
			_context = context;
		}

		public async Task<ICollection<StoredFileDetails?>> GetAllByStoredFilesAsync(ICollection<StoredFile> storedFiles)
		{
			List<StoredFileDetails?> result = new();

			foreach (var storedFile in storedFiles)
			{
				var itemToAdd = await _context.StoredFilesDetails.FirstOrDefaultAsync(sfd => sfd.StoredFileId == storedFile.Id);
				result.Add(itemToAdd);
			}

			return result;
		}
	}
}
