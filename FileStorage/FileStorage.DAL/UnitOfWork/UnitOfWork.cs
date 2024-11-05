using FileStorage.DAL.Data;
using FileStorage.DAL.Repositories.Interfaces;

namespace FileStorage.DAL.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly FileStorageDbContext _context;

		public IUserRepository Users { get; }
		public IStoredFileRepository StoredFiles { get; }
		public IStoredFileDetailsRepository StoredFilesDetails { get; }


		public UnitOfWork(FileStorageDbContext context, 
						  IUserRepository users, 
						  IStoredFileRepository storedFiles, 
						  IStoredFileDetailsRepository storedFilesDetails)
		{
			_context = context;
			Users = users;
			StoredFiles = storedFiles;
			StoredFilesDetails = storedFilesDetails;
		}

		public async Task CommitAsync()
		{
			await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}
	}
}
